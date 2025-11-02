namespace Multithreading_Examples.Primitives
{
    internal class Monitor_Counter
    {
        int _counter = 0;
        int _counterLocked = 0;

        object _lock = new object();

        internal Monitor_Counter()
        {
            Thread thread1 = new Thread(IncrementCounter);
            Thread thread2 = new Thread(IncrementCounter);
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"Final counter without lock value is: {_counter}");

            Thread thread3 = new Thread(IncrementCounter_Locked);
            Thread thread4 = new Thread(IncrementCounter_Locked);
            thread3.Start();
            thread4.Start();

            thread3.Join();
            thread4.Join();

            Console.WriteLine($"Final counter with lock value is: {_counterLocked}");
            Console.ReadLine();
        }

        void IncrementCounter()
        {
            for (int i = 0; i < 100000; i++)
            {
                _counter++;
            }
        }

        void IncrementCounter_Locked()
        {
            for (int i = 0; i < 100000; i++)
            {
                // this does the same as lock(_lock) { ... }
                Monitor.Enter(_lock);
                try
                {
                    _counterLocked++;
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
    }
}
