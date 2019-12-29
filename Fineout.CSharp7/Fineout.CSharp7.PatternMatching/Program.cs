using System;

namespace Fineout.CSharp7.PatternMatching
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    /*
     * Patterns are a new language element in C# 7, with many new features expected in the future
     * 
     * 
     * Constant patterns: of form c (where c is a constant expression), which tests for equality
     *      Example: null
     *      
     * Type patterns: of form T x (where T is a type and x an identifier), which tests that
     *                input has type T and extracts the value into new variable x of type T
     *      Example: int i
     * 
     * Var patterns: of form var x (where x is an identifier), which always matches and extracts
     *               the value of the input into new variable x with same type as the input
     *      Example: var y
     * 
     * 
     * Two new features supported in C# 7 - enhancement to existing language constructs:
     *  - Is-expressions with patterns - is expression now supports patterns along with types
     *  - Switch statements with patterns - case clauses can now match patterns along with constants
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Is-expressions with patterns:");
            PrintStars(7);
            Console.WriteLine();

            Console.WriteLine("Pattern matching combined with out variables and \"try\" method:");
            PrintStars_WithTry(7);
            Console.WriteLine();

            Console.WriteLine("Switch statements with patterns:");
            PrintShape(new Circle(7));
            PrintShape(new Rectangle(6, 6));
            PrintShape(new Rectangle(7, 42));
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void PrintStars(object o)
        {
            // constant pattern "null"
            if (o is null)
            {
                return;
            }

            // type pattern "int i"
            if (!(o is int i))
            {
                return;
            }

            // Note that the pattern variable is introduced into enclosing scope!
            Console.WriteLine(new string('*', i));
        }

        private static void PrintStars_WithTry(object o)
        {
            // Combine pattern matching with out variables and "try" method
            if (o is int i ||
                o is string s && int.TryParse(s, out i))
            {
                // Note that the pattern variable and out variable do not conflict!
                Console.WriteLine(new string('*', i));
            }
        }

        private static void PrintShape(Shape shape)
        {
            // Note that order of clauses now matters! The square comparison below will
            // always be evaluated before the other rectangle clause!
            switch (shape)
            {
                case Circle c:
                    // Pattern variables introduced in case clauses are in scope only within the
                    // corresponding switch section:
                    Console.WriteLine($"circle with radius {c.Radius}");
                    break;
                
                // Case clauses can have additional conditions using "when"!
                //case Rectangle s when (s.Length == s.Height):
                //    Console.WriteLine($"{s.Length} x {s.Height} square");
                //    break;

                case Rectangle r:
                    if (r.Length == r.Height)
                    {
                        Console.WriteLine($"{r.Length} x {r.Height} square");
                    }
                    Console.WriteLine($"{r.Length} x {r.Height} rectangle");
                    break;
                    
                // Default clause is always evaluated last!!
                default:
                    Console.WriteLine("<unknown shape>");
                    break;
                
                // Null clause is not unreachable!
                case null:
                    throw new ArgumentNullException(nameof(shape));
            }
        }
    }

    public class Shape {}

    public class Rectangle : Shape
    {
        public double Length;
        public double Height;

        public Rectangle(double length, double height)
        {
            Length = length;
            Height = height;
        }
    }

    public class Circle : Shape
    {
        public double Radius;

        public Circle(double radius)
        {
            Radius = radius;
        }
    }
}
