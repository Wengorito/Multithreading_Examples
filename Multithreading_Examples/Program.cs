Console.WriteLine("Enter a command to run a multithreading primitive example (lock, monitor, mutex, semaphore, autoresetevent, manualresetevent) or 'exit' to quit:");

string? input = Console.ReadLine();
if (input?.ToLower() == "exit")
{
    return;
}
switch (input)
{
    case "lock":
        new Multithreading_Examples.Primitives.Lock();
        break;
    //case "monitor":
    //    new Multithreading_Examples.Primitives.Monitor();
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
