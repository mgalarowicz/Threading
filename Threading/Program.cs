using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static object baton = new object();

        static void Main(string[] args)
        {
            //lock (baton)
            bool lockTaken = false;
            Monitor.Enter(baton, ref lockTaken);
            try
            {
                Console.WriteLine("Got the baton");
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(baton);
            }
        }
    }
}
