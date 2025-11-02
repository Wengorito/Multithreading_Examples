namespace Multithreading_Examples.Primitives
{
    internal class MonitorTimeout_TicketsBooking
    {
        Queue<string?> requestQueue = new Queue<string?>();

        int _availableTickets = 10;
        object _ticketsLock = new object();

        internal MonitorTimeout_TicketsBooking()
        {
            // 1. Start the requests queue monitoring thread
            Thread monitoringThread = new Thread(MonitoringQueue);
            monitoringThread.Start();

            // 2. Enqueue the requests
            Console.WriteLine("Server is running. \r\n Type 'b' to book a ticket. \r\n Type 'c' to cancel. \r\n Type 'exit' to stop. \r\n");
            while (true)
            {
                string? input = Console.ReadLine() ?? "";
                if (input?.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting application...");
                    break;
                }
                // add lock/monitor to protect the queue
                requestQueue.Enqueue(input);
            }
        }

        void MonitoringQueue()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    // add lock/monitor to protect the queue
                    string? input = requestQueue.Dequeue();
                    Thread processingThread = new Thread(() => ProcessBooking(input));
                    processingThread.Start();
                }
                Thread.Sleep(100);
            }
        }

        // 3. Processing the requests
        void ProcessBooking(string? input)
        {
            if (Monitor.TryEnter(_ticketsLock, 2000))
                try
                {
                    // Simulate processing time
                    Thread.Sleep(3000);

                    if (input == "b")
                    {
                        if (_availableTickets > 0)
                        {
                            _availableTickets--;
                            Console.WriteLine();
                            Console.WriteLine($"Your seat is booked. {_availableTickets} seats are still available.");
                        }
                        else
                        {
                            Console.WriteLine($"Tickets are not available.");
                        }
                    }
                    else if (input == "c")
                    {
                        if (_availableTickets < 10)
                        {
                            _availableTickets++;
                            Console.WriteLine();
                            Console.WriteLine($"Your booking is canceled. {_availableTickets} seats are available.");
                        }
                        else
                        {
                            Console.WriteLine($"Error. You cannot cancel a booking at this time.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
                finally
                {
                    Monitor.Exit(_ticketsLock);
                }
            else
            {
                Console.WriteLine("The system is busy. Please wait.");
            }
        }
    }
}
