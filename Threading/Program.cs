using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(DifferentMethod);
            thread.Start();
        }

        static void DifferentMethod()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
