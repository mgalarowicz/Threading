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

        private static void TheWeirdGuy()
        {
            stall.BeUsed(99);
        }

        static void RegularUsers()
        {
            //lock means - "are you gonna respect my privacy?" :) 
            //lock (stall)
                stall.BeUsed(Thread.CurrentThread.ManagedThreadId);
        }
        
    }
}
