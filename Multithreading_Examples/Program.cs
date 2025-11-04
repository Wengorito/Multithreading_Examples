
Console.WriteLine("Enter the command number and press enter to run a multithreading primitive example:\n\n" +
"1. simple 2-threaded counter (monitor)\n2. multi-threaded server simulation (lock)\n3. tickets booking system (monitor with timeout)\n" +
"4. mutex\n5. semaphore\n6. autoresetevent with worker thread" +
"\n7. manualresetevent with worker threads" +
"\n8. simple consumer producer (AutoResetEvent)\n9. Consumer Producer with pause (Auto + Manual)" +
"\n10. ProducerConsumer with quit (AutoResetEvent with CountDownEvent)" +
"\nor 'exit' to quit:\n\n");

while (true)
{
    Console.BackgroundColor = ConsoleColor.Cyan;
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Type your command and press enter: ");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.White;

    string? input = Console.ReadLine();
    if (input?.ToLower() == "exit")
    {
        break;
    }
    switch (input)
    {
        case "1":
            new Multithreading_Examples.Primitives.Monitor_Counter();
            break;
        case "2":
            new Multithreading_Examples.Primitives.Lock_WebServer();
            break;
        case "3":
            new Multithreading_Examples.Primitives.MonitorTimeout_TicketsBooking();
            break;
        case "4":
            new Multithreading_Examples.Primitives.Mutex_CounterWithReadWrite();
            break;
        case "5":
            var semaphoreExample = new Multithreading_Examples.Primitives.Semaphore_WebServer();
            semaphoreExample.Run();
            break;
        case "6":
            new Multithreading_Examples.Primitives.ResetEvent.AutoResetEvent_WorkerThread();
            break;
        case "7":
            new Multithreading_Examples.Primitives.ResetEvent.ManualResetEvent_WorkerThreads();
            break;
        case "8":
            new Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos.ProducerConsumer_Simple();
            break;
        case "9":
            new Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos.ProducerConsumer_WithPause();
            break;
        case "10":
            new Multithreading_Examples.Primitives.ResetEvent.ProducerConsumerDemos.ProducerConsumer_WithQuit();
            break;
        default:
            Console.WriteLine("Unknown command. Available commands: lock, monitor, mutex, semaphore, autoresetevent, manualresetevent");
            break;
    }
}

