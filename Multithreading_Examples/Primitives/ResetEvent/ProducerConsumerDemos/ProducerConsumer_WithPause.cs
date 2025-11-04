namespace Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos;

internal class ProducerConsumer_WithPause
{
    // the array of consumer threads
    private static List<Thread> consumers = new List<Thread>();

    // the task queue
    private static Queue<Action> tasks = new Queue<Action>();

    // the synchronisation object for locking the task queue
    private static readonly object queueLock = new object();

    // this wait handle notifies consumers of a new task
    private static EventWaitHandle newTaskAvailable = new AutoResetEvent(false);

    // this wait handle pauses consumers
    private static EventWaitHandle pauseConsumers = new ManualResetEvent(true);

    // the synchronisation object for locking the console color
    private static readonly object consoleLock = new object();

    internal ProducerConsumer_WithPause()
    {
        // set up 3 consumers
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Red); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Green); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Blue); }));

        // start all consumers
        consumers.ForEach((t) => { t.Start(); });

        bool consumersPaused = false;

        Console.WriteLine("Hit enter to pause or resume threads");

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

            // pressing any key pauses/unpauses the consumers
            if (Console.KeyAvailable)
            {
                Console.ReadLine();
                if (consumersPaused)
                {
                    pauseConsumers.Set();
                    Console.WriteLine("Consumers resumed");
                }
                else
                {
                    pauseConsumers.Reset();
                    Console.WriteLine("Consumers paused");
                }

                consumersPaused = !consumersPaused;
            }
        }
    }

    // enqueue a new task
    private static void EnqueueTask(Action task)
    {
        lock (queueLock)
        {
            tasks.Enqueue(task);
        }
        newTaskAvailable.Set();
    }


    // thread work method for consumers
    private static void DoWork(ConsoleColor color)
    {
        while (true)
        {
            // check if producer asked us to pause
            pauseConsumers.WaitOne();

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
