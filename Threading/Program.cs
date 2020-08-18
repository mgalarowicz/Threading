using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Threading
{
    class BathroomStall
    {
        public void BeUsed(int userNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Being used by " + userNumber);
                Thread.Sleep(500);
            }
        }
    }

    class Program
    {
        static BathroomStall stall = new BathroomStall();

        static void Main()
        {
            for (int i = 0; i < 3; i++)
            {
                new Thread(RegularUsers).Start();
            }
            new Thread(TheWeirdGuy).Start();
        }

        static void RegularUsers()
        {
            lock (stall)
                stall.BeUsed(Thread.CurrentThread.ManagedThreadId);
        }

        private static void TheWeirdGuy()
        {
            //lock means - "are you gonna respect my privacy?" :) 
            lock (stall)
                stall.BeUsed(99);
        }

    }
}
