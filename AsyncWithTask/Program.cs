using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncWithTask
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Ex 1

            //DateTime dateTime = DateTime.Now;
            //Thread.Sleep(1000);

            //Task task1 = new Task(() => { Console.WriteLine(dateTime); });
            //task1.Start();
            //task1.Wait();

            //Task task2 = Task.Factory.StartNew(PrintDateTime);
            //task2.Wait();

            //Task task3 = Task.Run(() => Console.WriteLine(dateTime));

            #endregion


            #region Ex 2

            //Console.WriteLine("Main thread start waiting");

            //Task task = Task.Factory.StartNew(PrintPrime);

            //task.Wait();

            //Console.WriteLine("\nMain thread end waiting");


            #endregion


            #region Ex 3


            //Console.WriteLine("Main thread start waiting");

            //Task task = Task.Factory.StartNew(PrinPrimeWithBoudries);

            //task.Wait();

            //Console.WriteLine("\nMain thread end waiting");

            #endregion


            #region Ex 4

            List<int> nums = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                int n = rnd.Next(1, 1000);

                if (!nums.Contains(n)) nums.Add(n);
            }

            Task[] tasks = new Task[4]
            {
                new Task(FindMax),
                new Task(FindMin),
                new Task(FindAverage),
                new Task(FindSum)
            };



            #endregion


        }

        private static void PrintDateTime()
        {
            Console.WriteLine(DateTime.Now);
        }


        public static void PrintPrime()
        {
            for (int i = 0; i < 1000; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
        }

        
        public static void PrinPrimeWithBoudries()
        {
            Console.Write("Print lower boundry: ");
            int lb = Int32.Parse( Console.ReadLine());
            Console.Write("Print upper boundry: ");
            int ub = Int32.Parse(Console.ReadLine());

            for (int i = lb; i < ub; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
        }
        

        public static void FindMax()
        {

        }

        public static void FindMin()
        {

        }

        public static void FindAverage()
        {

        }

        public static void FindSum()
        {

        }


        public static bool IsPrime(int n)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            int half = (int)Math.Floor(Math.Sqrt(n));

            for (int i = 3; i <= half; i += 2)
                if (n % i == 0)
                    return false;

            return true;
        }

    }
}
