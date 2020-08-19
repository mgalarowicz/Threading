using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Threading
{
    class BathroomStall
    {
        [MethodImpl(MethodImplOptions.Synchronized)] //allow to use that method only by one thread at the time. It's quite the same behavior as lock(this). 
        public static void BeUsed()  //in this case (with MethodImpl and static) I'm locking on a type! It's really global! lock (typeof(BathroomStall))
        {
            Console.WriteLine("Doing our thing...");
            Thread.Sleep(5000);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void FlushToilet()
        {
            Console.WriteLine("Flushing the toilet...");
            Thread.Sleep(5000);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var stall = new BathroomStall();
            new Thread(stall.BeUsed).Start();
            new Thread(stall.FlushToilet).Start();

        }
    }
}

