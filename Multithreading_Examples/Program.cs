Console.WriteLine("Enter a command to run a multithreading primitive example:\n\n" +
    "1. simple 2-threaded counter (monitor)\n2. multi-threaded server simulation (lock)\n3. tickets booking system (monitor with timeout)\n4. mutex\n5. semaphore\n6. autoresetevent\n7. manualresetevent\nor 'exit' to quit:");

string? input = Console.ReadLine();
if (input?.ToLower() == "exit")
{
    return;
}
switch (input)
{
    case "1":
        new Multithreading_Examples.Primitives.Monitor_Counter();
        break;
    case "2":
        new Multithreading_Examples.Primitives.Lock_WebServer();
        break;
    //case "3":
    //    new Multithreading_Examples.Primitives.MonitorTimeout_TicketsBooking();
    //    break;
    //case "mutex":
    //    new Multithreading_Examples.Primitives.MutexExample();
    //    break;
    //case "semaphore":
    //    new Multithreading_Examples.Primitives.SemaphoreExample();
    //    break;
    //case "autoresetevent":
    //    new Multithreading_Examples.Primitives.AutoResetEventExample();
    //    break;
    //case "manualresetevent":
    //    new Multithreading_Examples.Primitives.ManualResetEventExample();
    //    break;
    default:
        Console.WriteLine("Unknown command. Available commands: lock, monitor, mutex, semaphore, autoresetevent, manualresetevent");
        break;
}
