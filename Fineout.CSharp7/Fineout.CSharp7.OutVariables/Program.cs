using System;

namespace Fineout.CSharp7.OutVariables
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    public class Program
    {
        public static void Main(string[] args)
        {
            var point = new Point(7, 42);

            Console.WriteLine("Old Way:");
            PrintCoordinates_Old(point);
            Console.WriteLine();

            Console.WriteLine("Shiny C# 7 Way:");
            PrintCoordinates_CS7(point);
            Console.WriteLine();

            Console.WriteLine("Example with \"try\" method:");
            PrintStars("7");
            Console.WriteLine();

            Console.WriteLine("Example with discarded parameter:");
            PrintXCoordinate(point);
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void PrintCoordinates_Old(Point p)
        {
            // Old way: have to "predeclare" out variables
            int x, y;
            p.GetCoordinates(out x, out y);
            Console.WriteLine($"({x}, {y})");
        }

        private static void PrintCoordinates_CS7(Point p)
        {
            // Shiny new way: no need to predeclare!
            p.GetCoordinates(out int x, out int y);

            // Note that the declared out variables are introduced into the enclosing scope
            Console.WriteLine($"({x}, {y})");

            // We can also implicitly type, as long as there are no ambiguous overloads
            p.GetCoordinates(out var a, out var b);
            Console.WriteLine($"({a}, {b})");
        }

        private static void PrintStars(string s)
        {
            if (int.TryParse(s, out var i))
            {
                Console.WriteLine(new string('*', i));
            }
            else
            {
                Console.WriteLine("Cloudy - no stars tonight!");
            }
        }

        private static void PrintXCoordinate(Point p)
        {
            // If we don't care about an out parameter, we can discard it with "_"
            p.GetCoordinates(out var x, out _); // I only care about x
            Console.WriteLine($"{x}");
        }
    }
    
    public class Point
    {
        public int X;
        public int Y;

        public void GetCoordinates(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
