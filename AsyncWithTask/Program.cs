using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncWithTask
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();


                Console.WriteLine("Enter number of exercise(1 - 5)");
                Console.WriteLine("Enter '0' to exit");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.Write("Ex #: ");
                string ch = Console.ReadLine();

                if (ch == "1") StartEx1();

                else if (ch == "2") StartEx2();

                else if (ch == "3") StartEx3();

                else if (ch == "4") StartEx4();

                else if (ch == "5") StartEx5();

                else if (ch == "0") break;


                Console.WriteLine("In main thread");
                Console.WriteLine("Enter any key to continue");
                Console.ReadKey();
            }
        }

        private static void StartEx1()
        {
            DateTime dateTime = DateTime.Now;

            Console.Write("Process 1: ");
            Task task1 = new Task(() => { Console.WriteLine(dateTime); });
            task1.Start();
            task1.Wait();


            Console.Write("Process 2: ");
            Task task2 = Task.Factory.StartNew(PrintDateTime);
            task2.Wait();

            Console.Write("Process 3: ");
            Task task3 = Task.Run(() => Console.WriteLine(dateTime));
            task3.Wait();
        }

        private static void StartEx2()
        {
            Console.WriteLine("Main thread start waiting\n");

            Task task = Task.Factory.StartNew(PrintPrime);

            task.Wait();

            Console.WriteLine("\n\nMain thread stop waiting");
        }

        public static void StartEx3()
        {
            Console.WriteLine("Main thread start waiting\n");

            Task task = Task.Factory.StartNew(PrintPrimeWithBoudries);

            task.Wait();

            Console.WriteLine("\n\nMain thread stop waiting");
        }

        public static void StartEx4()
        {
            List<int> list = new List<int>();
            Random rnd = new Random();
            Console.WriteLine();
            Console.WriteLine("List");
            Console.WriteLine("~~~~~~~~~~~~~~~");
            for (int i = 0; i < 100; i++)
            {
                list.Add(rnd.Next(1, 1000));
                Console.Write(list[i] + " ");
            }

            Task[] tasks = new Task[4]
            {
                new Task(() => PrintMax(list)),
                new Task(() => PrintMin(list)),
                new Task(() => PrintAverage(list)),
                new Task(() => PrintSum(list))
            };


            Console.WriteLine("\n\nMain thread start waiting\n");


            for (int i = 0; i < 4; i++)
            {
                tasks[i].Start();
                tasks[i].Wait();
            }


            Console.WriteLine("\nMain thread stop waiting");
        }

        public static void StartEx5()
        {
            try
            {
                List<int> list = new List<int>();
                Random rnd = new Random();
                Console.WriteLine();
                Console.WriteLine("List");
                Console.WriteLine("~~~~~~~~~~~~~~~");
                for (int i = 0; i < 100; i++)
                {
                    list.Add(rnd.Next(1, 1000));
                    Console.Write(list[i] + " ");
                }

                Console.Write($"\n\nEnter number to find after sort: ");
                int num = Int32.Parse(Console.ReadLine());

                Task<List<int>> task1 = Task.Run(() => RemoveSameElements(list));

                Task<List<int>> task2 = task1.ContinueWith<List<int>>((x) => Sort(x.Result));

                Task<int> task3 = task2.ContinueWith<int>((x) => Search(x.Result, num));

                Task task4 = task3.ContinueWith((x) => { Console.WriteLine($"\nIndex after sort: {x.Result}"); });


                Console.WriteLine("Main thread start waiting");
                task4.Wait();
                Console.WriteLine("\nMain thread stop waiting");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\nMessage: {ex.Message}\n\n\nStack Trace: {ex.StackTrace}\n\n");
            }
        }




        private static void PrintDateTime()
        {
            Console.WriteLine(DateTime.Now);
        }

        public static void PrintPrime()
        {
            Console.WriteLine("Prime numbers");
            Console.WriteLine("~~~~~~~~~~~~~~~"); 
            for (int i = 0; i < 1000; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
        }

        public static void PrintPrimeWithBoudries()
        {
            Console.Write("Lower boundry: ");
            int lb = Int32.Parse(Console.ReadLine());
            Console.Write("Upper boundry: ");
            int ub = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\nPrime numbers");
            Console.WriteLine("~~~~~~~~~~~~~~~");
            for (int i = lb; i < ub; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
        }

        public static List<int> RemoveSameElements(List<int> nums)
        {
            return nums.Distinct().ToList();
        }

        public static List<int> Sort(List<int> nums)
        {
            nums.Sort();

            Console.WriteLine();
            Console.WriteLine("Sorted list");
            Console.WriteLine("~~~~~~~~~~~~~~~");
            for (int i = 0; i < nums.Count; i++)
            {
                Console.Write(nums[i] + " ");
            }

            Console.WriteLine();

            return nums;
        }

        public static int Search(List<int> nums, int numToSearch)
        {
            return nums.BinarySearch(numToSearch);
        }


        public static void PrintMax(List<int> nums)
        {
            Console.WriteLine("Max: " + nums.Max());
        }

        public static void PrintMin(List<int> nums)
        {
            Console.WriteLine("Min: " + nums.Min());
        }

        public static void PrintAverage(List<int> nums)
        {
            Console.WriteLine("Average: " + nums.Average());
        }

        public static void PrintSum(List<int> nums)
        {
            Console.WriteLine("Sum: " + nums.Sum());
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
