namespace Multithreading_Examples.Primitives;

internal class AutoResetEvent_ProducerConsumer
{
    ///////////////////////////////////////////////////////////////////////
    // Multiple Worker thread
    /////////////////////////////////////////////////////////////////////// 

    string? _userInput = null;
    internal AutoResetEvent_ProducerConsumer()
    {
        using AutoResetEvent autoResetEvent = new AutoResetEvent(false);

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

    void Worker(AutoResetEvent autoResetEvent)
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
