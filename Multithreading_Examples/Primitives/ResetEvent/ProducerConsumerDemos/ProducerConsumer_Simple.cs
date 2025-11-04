namespace Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos;

class ProducerConsumer_Simple
{
    // the array of consumer threads
    private static List<Thread> consumers = new List<Thread>();

    // the task queue
    private static Queue<Action> tasks = new Queue<Action>();

    // żeby nie lockować ręcznie, można użyć ConcurrentQueue
    //private static ConcurrentQueue<Action> tasks = new ConcurrentQueue<Action>();

    // the synchronisation object for locking the task queue
    private static readonly object queueLock = new object();

    // this wait handle notifies consumers of a new task
    private static EventWaitHandle newTaskAvailable = new System.Threading.AutoResetEvent(false);

    // the synchronisation object for locking the console color
    private static readonly object consoleLock = new object();

    internal ProducerConsumer_Simple()
    {
        // set up 3 consumers
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Red); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Green); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Blue); }));

        // start all consumers
        consumers.ForEach((t) => { t.Start(); });

        while (true)
        {
            // add a new task
            Random r = new Random();
            EnqueueTask(() =>
            {

                // the task is to write a random number to the console
                int number = r.Next(10);
                Console.Write(number);

            });

            // random sleep to simulate workload
            Thread.Sleep(r.Next(1000));
        }
    }

    private static void EnqueueTask(Action task)
    {
        tasks.Enqueue(task);

        lock (queueLock)
        {
            newTaskAvailable.Set();
        }
    }

    // thread work method for consumers
    private static void DoWork(ConsoleColor color)
    {
        while (true)
        {
            // get a new task
            Action task = null;
            lock (queueLock)
            {
                if (tasks.Count > 0)
                {
                    task = tasks.Dequeue();
                }
            }
            if (task != null)
            {
                // set console to this task's color
                lock (consoleLock)
                {
                    Console.ForegroundColor = color;
                }

                // execute task
                task();
            }
            else
                // queue is empty - wait for a new task
                newTaskAvailable.WaitOne();
        }
    }
}
