using System;
using System.Collections.Generic;
using System.Threading;

namespace Threading
{
    //In threading two separte operations (non atomic) are always a Danger Zone! For example:
    //if (numbers.Count != 0)
    //{
    //      RIGHT HERE, DANGER ZONE!
    //    numToSum = numbers.Dequeue();
    //}

class Program
    {
        static MySynchronizedQueue<int> numbers = new MySynchronizedQueue<int>();
        static Random rand = new Random(999);
        const int NumThreads = 3;
        static int[] sums = new int[NumThreads];

        static void ProduceNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                int numToEnqueue = rand.Next(10);
                Console.WriteLine($"Producing thread adding {numToEnqueue} to the queue.");
                //indirect way of access synchronization block (through an object on the heap)
                lock (numbers)
                    numbers.Enqueue(numToEnqueue);
                Thread.Sleep(rand.Next(1000));
            }
        }

        static void SumNumbers(object threadNumber)
        {
            DateTime startTime = DateTime.Now;
            int mySum = 0;
            while ((DateTime.Now - startTime).Seconds < 11)
            {
                int numToSum = -1;

                lock (numbers.SyncRoot)
                {
                    if (numbers.Count != 0)
                    {
                        numToSum = numbers.Dequeue();
                    }
                }

                if (numToSum > -1)
                {
                    mySum += numToSum;
                    Console.WriteLine($"Consuming thread #{threadNumber} adding {numToSum} to its total sum" +
                                      $"making {mySum} for the thread total.");
                }
            }
            sums[(int)threadNumber] = mySum;
        }

        static void Main(string[] args)
        {
            var producingThread = new Thread(ProduceNumbers);
            producingThread.Start();
            Thread[] threads = new Thread[NumThreads];

            for (int i = 0; i < NumThreads; i++)
            {
                threads[i] = new Thread(SumNumbers);
                threads[i].Start(i);
            }

            for (int i = 0; i < NumThreads; i++)
            {
                threads[i].Join();
            }

            int totalSum = 0;
            for (int i = 0; i < NumThreads; i++)
            {
                totalSum += sums[i];
            }

            Console.WriteLine($"Done adding. Total is {totalSum}");

        }
    }

    class MySynchronizedQueue<T>
    {
        object baton = new object();
        Queue<T> theQ = new Queue<T>();

        public void Enqueue(T item)
        {
            lock(baton)
                theQ.Enqueue(item);
        }

        public T Dequeue()
        {
            lock (baton)
                return theQ.Dequeue();
        }

        public int Count
        {
            get { 
            lock (baton)
                return theQ.Count;
            }
        }
        public object SyncRoot
        {
            get { return baton; }
        }
    }
}

