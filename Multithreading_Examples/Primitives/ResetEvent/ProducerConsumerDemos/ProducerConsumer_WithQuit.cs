namespace Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos;

internal class ProducerConsumer_WithQuit
{
    // the array of consumer threads
    private static List<Thread> consumers = new List<Thread>();

    // the task queue
    private static Queue<Action> tasks = new Queue<Action>();

    // the synchronisation object for locking the task queue
    private static readonly object queueLock = new object();

    // this wait handle notifies consumers of a new task
    private static EventWaitHandle newTaskAvailable = new AutoResetEvent(false);

    // the wait handle to quit consumers
    private static CountdownEvent quitConsumers = new CountdownEvent(3);

    // the flag to request that consumers quit
    private static bool quitRequested = false;

    // the synchronisation object for quitting consumers
    private static readonly object quitLock = new object();

    // the synchronisation object for locking the console color
    private static readonly object consoleLock = new object();

    internal ProducerConsumer_WithQuit()
    {
        // set up 3 consumers
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Red); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Green); }));
        consumers.Add(new Thread(() => { DoWork(ConsoleColor.Blue); }));

        // start all consumers
        consumers.ForEach((t) => { t.Start(); });

        int iterations = 0;

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

            // quit after 10 iterations
            if (iterations++ >= 10)
            {
                // request consumer quit
                lock (quitLock)
                {
                    quitRequested = true;
                }

                // wait until all consumers have signalled
                quitConsumers.Wait();

                Console.WriteLine("All consumers have quit");
                break;
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
            // check if someone asked us to quit
            lock (quitLock)
            {
                if (quitRequested)
                {
                    Console.WriteLine("Consumer {0} is quitting", color);
                    quitConsumers.Signal();
                    break;
                }
            }

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
                newTaskAvailable.WaitOne(1000);
        }
    }
}
