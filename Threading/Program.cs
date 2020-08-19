using System;
using System.Collections.Generic;
using System.Threading;

namespace Threading
{
    class BathroomStall
    { 
    object baton = new object();

        public void BeUsed()
        {
            //lock (this)  if i lock on this, then for example if I lock reference to an object BathroomStall (outside) in method
            //CleanSink then I will block to use my methods BeUsed and FlushToilet, and that do not make sense. Thats why we used baton
            lock (baton)
                Console.WriteLine("Doing our thing...");
        }
        public void FlushToilet()
        {
            lock (baton)
                Console.WriteLine("Flushing the toilet...");
        }
    }

    class PublicRestroom
    {
        BathroomStall stall1 = new BathroomStall();
        BathroomStall stall2 = new BathroomStall();

        public void UseStall1()
        {
            lock (stall1)
            {
                stall1.BeUsed();
                stall1.FlushToilet();
            }
        }

        public void UseStall2()
        {
            stall2.BeUsed();
            stall2.FlushToilet();
        }

        public void CleanSink()
        {
            lock (stall1)
                Console.WriteLine("Cleaning sink...");
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var restroom = new PublicRestroom();
            new Thread(restroom.UseStall1).Start();
            new Thread(restroom.UseStall2).Start();
            new Thread(restroom.UseStall1).Start();
            new Thread(restroom.UseStall2).Start();
        }
    }
}

