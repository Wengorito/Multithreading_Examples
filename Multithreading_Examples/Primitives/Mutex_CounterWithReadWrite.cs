using System.Diagnostics;

namespace Multithreading_Examples.Primitives
{
    internal class Mutex_CounterWithReadWrite
    {
        string _filePath = "counter.txt";

        // run multiple example instances simultaneously (.exe in \bin folder) to see the effect
        internal Mutex_CounterWithReadWrite()
        {
            this.Increment(_filePath);

            Process.Start("notepad.exe", _filePath);
            Console.WriteLine("Process finished.");
            Console.ReadLine();
        }

        void Increment(string filePath)
        {
            for (int i = 0; i < 10000; i++)
            {
                // Mutex allows exclusion across different processes
                using (var mutex = new Mutex(false, $"GlobalFileMutex:{filePath}"))
                {
                    mutex.WaitOne();
                    try
                    {
                        int counter = ReadCounter(filePath);
                        counter++;
                        WriteCounter(filePath, counter);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }

        int ReadCounter(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
        }

        void WriteCounter(string filePath, int counter)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(counter);
            }
        }
    }
}
