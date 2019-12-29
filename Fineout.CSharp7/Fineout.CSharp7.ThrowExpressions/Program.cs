using System;

namespace Fineout.CSharp7.ThrowExpressions
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Throw in Null-Coalesce Expression:");
            try
            {
                var p = new Person(null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Caught an ArgumentNullException!");
            }
            Console.WriteLine();

            Console.WriteLine("Throw in Tertiary If Expression:");
            try
            {
                var p = new Person(string.Empty);
                var name = p.GetFirstName();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Caught an InvalidOperationException!");
            }
            Console.WriteLine();

            Console.WriteLine("Throw in Expresison-Bodied Method:");
            try
            {
                var p = new Person("Giuseppe Zanotti");
                var name = p.GetLastName();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Caught an NotImplementedException!");
            }
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();

        }
    }

    public class Person
    {
        public string Name { get; }

        // Throw an exception if name is null, using a null-coalesce expression
        // Bonus points for expression-bodied constructor!
        public Person(string name) =>
            Name = name
            ?? throw new ArgumentNullException(nameof(name));

        public string GetFirstName()
        {
            var parts = Name.Split(' ');

            // Throw an exception if parts has no elements, using a tertiary if expression
            return parts.Length > 1
                ? parts[0]
                : throw new InvalidOperationException("No name!");
        }

        // Throw an exception as a expression-bodied method
        public string GetLastName() => throw new NotImplementedException();
    }
}
