using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Threading
{
    class Program
    {
        static byte[] values = new byte[500000000];
        static void GenerateInts()
        {
            var rand = new Random(987);
            for (int i = 0; i < values.Length; i++)
                values[i] = (byte)rand.Next(10);
        }

        static void Main(string[] args)
        {
            GenerateInts();
            Console.WriteLine("Summing...");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long total = values.Aggregate<byte, long>(0, (current, t) => current + t);
            watch.Stop();
            Console.WriteLine("Total value is: " + total);
            Console.WriteLine("Time to sum: " + watch.Elapsed);
        }
    }
}
