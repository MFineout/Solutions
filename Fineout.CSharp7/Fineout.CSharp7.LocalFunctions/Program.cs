using System;
using System.Collections.Generic;
using System.Linq;

namespace Fineout.CSharp7.LocalFunctions
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Local Functions - Fibonnaci:");
            Console.WriteLine($"Fibonnaci(1) = {Fibonacci(1)}");
            Console.WriteLine($"Fibonnaci(3) = {Fibonacci(3)}");
            Console.WriteLine($"Fibonnaci(12) = {Fibonacci(12)}");
            Console.WriteLine();

            Console.WriteLine("Local Function - Iterator:");
            int[] nums = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var even = Filter(nums, n => n % 2 == 0);
            Console.WriteLine($"Even: {string.Join(", ", even.ToArray())}");
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static int Fibonacci(int x)
        {
            if (x < 0) throw new ArgumentException("Less negativity please!", nameof(x));
            return Fib(x).current;

            // Local functions, yay!
            (int current, int previous) Fib(int i)
            {
                if (i == 0)
                {
                    return (1, 0);
                }

                var (p, pp) = Fib(i - 1);
                return (p + pp, p);
            }
        }

        private static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return Iterator();

            // Local function as an iterator, instead of needing a wrapping non-iterator function
            IEnumerable<T> Iterator()
            {
                foreach (var element in source)
                {
                    if (filter(element)) { yield return element; }
                }
            }

            // If Iterator had been a private method next to Filter, it would have been available
            // for other members to accidentally use directly (without argument checking).
            // Also, it would have needed to take all the same arguments as Filter instead of
            // having them just be in scope.
        }
    }
}
