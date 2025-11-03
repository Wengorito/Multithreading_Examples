
Console.WriteLine("Enter a command to run a multithreading primitive example:\n\n" +
"1. simple 2-threaded counter (monitor)\n2. multi-threaded server simulation (lock)\n3. tickets booking system (monitor with timeout)\n" +
"4. mutex\n5. semaphore\n6. autoresetevent\n7. manualresetevent\nor 'exit' to quit:\n\n");

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
}

