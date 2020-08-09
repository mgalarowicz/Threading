using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                var thread = new Thread(DifferentMethod);
                //ForeGround threads are the 'main' threads and console will not shot down after the end of the Main() thread.
                //In other hand, Backround threads will be terminated with console atfer Main() thread comes to the end.
                thread.IsBackground = true;
                thread.Start(i);
            }

            Console.WriteLine("Leaving Main");   
        }

        static void DifferentMethod(object threadId)
        {
            while (true)
                Console.WriteLine($"Hello from different method: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
