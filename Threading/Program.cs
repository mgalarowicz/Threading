using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                var thread = new Thread(DifferentMethod);
                thread.Start(i);
            }
            
            while (true)
                Console.WriteLine("Hello from Main()");
        }

        static void DifferentMethod(object threadId)
        {
            while (true)
                Console.WriteLine($"Hello from different method: {threadId}");
        }
    }
}
