Queue<string?> requestQueue = new Queue<string?>();
object queueLock = new object();

// 2. Start the requests queue monitoring thread
Thread monitoringThread = new Thread(MonitorQueue);
monitoringThread.Start();

// 1. Enqueue the requests
Console.WriteLine("Server is running. Type any input to process or 'exit' to stop.");
while (true)
{
    string? input = Console.ReadLine();
    if (input?.ToLower() == "exit")
    {
        break;
    }
    lock (queueLock)
    {
        requestQueue.Enqueue(input);
    }
}

void MonitorQueue()
{
    while (true)
    {
        if (requestQueue.Count > 0)
        {
            lock (queueLock)
            {
                string? input = requestQueue.Dequeue();
                Thread processingThread = new Thread(() => ProcessInput(input));
                processingThread.Start();
            }
        }
        Thread.Sleep(100);
    }
}

// 3. Processing the requests
void ProcessInput(string? input)
{
    // Simulate processing time
    Thread.Sleep(2000);
    Console.WriteLine($"Processed input: {input}");
}
