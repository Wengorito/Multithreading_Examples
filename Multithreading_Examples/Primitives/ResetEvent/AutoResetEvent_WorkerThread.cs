namespace Multithreading_Examples.Primitives.ResetEvent;

internal class AutoResetEvent_WorkerThread
{
    ///////////////////////////////////////////////////////////////////////
    // Multiple Worker thread
    /////////////////////////////////////////////////////////////////////// 

    string? _userInput = null;
    internal AutoResetEvent_WorkerThread()
    {
        using System.Threading.AutoResetEvent autoResetEvent = new System.Threading.AutoResetEvent(false);

        Console.WriteLine("Server is running. Type 'go' to proceed and  'exit' to stop.");

        // Start the worker thread
        for (int i = 0; i < 3; i++)
        {
            Thread workerThread = new Thread(() => Worker(autoResetEvent));
            workerThread.Name = $"Worker {i + 1}";
            workerThread.Start();
        }

        // Main thread (producer) receives user input
        while (true)
        {
            _userInput = Console.ReadLine();

            // Signal the worker threads (consumers) if input is "go"
            if (_userInput.ToLower() == "go")
            {
                autoResetEvent.Set();
            }
        }
    }

    void Worker(System.Threading.AutoResetEvent autoResetEvent)
    {
        while (true)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for signal.");
            // Wait for the signal from the main thread
            autoResetEvent.WaitOne();

            Console.WriteLine($"{Thread.CurrentThread.Name} proceeds.");
            // Simulate processing time
            Thread.Sleep(2000);
        }
    }
}
