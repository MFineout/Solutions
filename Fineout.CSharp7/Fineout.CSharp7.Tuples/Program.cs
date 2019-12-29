using System;
using System.Collections.Generic;

namespace Fineout.CSharp7.Tuples
{
    // See https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    /*
     *  C# 7.0 adds tuple types and tuple literals!
     *  
     *  Note that If you're targeting .NET 4.6.2 or lower or .NET Core,
     *  you need to install the NuGet package System.ValueTuple:
     *      Install-Package "System.ValueTuple"
     */

    public class Program
    {
        private static readonly Dictionary<int, Person> People = new Dictionary<int, Person>
        {
            {1, new Person("Dustin", "C.", "Fineout")},
            {2, new Person("Donald", "Ervin", "Knuth")},
            {3, new Person("Don", "Arthur", "Norman")},
            {4, new Person("Steven", "Paul", "Jobs")},
            {5, new Person("Stephen", "Gary", "Wozniak")},
            {6, new Person("Nolan", "Kay", "Bushnell")},
            {7, new Person("Ralph", "Henry", "Baer")}
        };

        public static void Main(string[] args)
        {
            Console.WriteLine("Old Way (out variables):");
            LookupName_Old(1, out string first, out string middle, out string last);
            Console.WriteLine($"{first} {middle} {last}");
            Console.WriteLine();

            Console.WriteLine("Tuples:");
            var tuple = LookupName_Tuple(2);
            // Note that by default, the tuple elements are named Item1, Item2, etc.
            Console.WriteLine($"{tuple.Item1} {tuple.Item2} {tuple.Item3}");
            Console.WriteLine();

            Console.WriteLine("Tuples (Named):");
            var tupleNamed = LookupName_TupleNamed(3);
            // Tuple element names can be descriptive as well
            Console.WriteLine($"{tupleNamed.First} {tupleNamed.Middle} {tupleNamed.Last}");
            Console.WriteLine();

            Console.WriteLine("Tuples (Named in Literal):");
            // Element names can be specified directly in the tuple literal
            var namedLiteral = (First: "Martin", Middle: "A.", Last: "Fowler");
            Console.WriteLine($"{namedLiteral.First} {namedLiteral.Middle} {namedLiteral.Last}");
            Console.WriteLine();

            Console.WriteLine("Tuple Value Equality:");
            TupleValueEquality();
            Console.WriteLine();

            Console.WriteLine("Tuple-keyed Dictionary:");
            TupleDictionary();
            Console.WriteLine();

            /*
             * Tuple Deconstruction
             * 
             * Tuples may also be consumed by deconstructing them. A deconstructing declaration is
             * new syntax for assiging the elements of a tuple into separate variables.
             */

            Console.WriteLine("Tuple Deconstructing Declaration:");
            TupleDeconstructingDeclaration();
            Console.WriteLine();

            Console.WriteLine("Tuple Deconstructing Declaration (Implicitly Typed):");
            TupleDeconstructingDeclaration_Implicit();
            Console.WriteLine();

            Console.WriteLine("Tuple Deconstructing Declaration (Implicitly Typed Entire Declaration):");
            TupleDeconstructingDeclaration_ImplicitAll();
            Console.WriteLine();

            Console.WriteLine("Tuple Deconstructing Assignment:");
            TupleDeconstructingAssignment();
            Console.WriteLine();

            /*
             * Deconstruction of Other Types
             * 
             * Any type can support deconstruction, with a "Deconstruct" method of the following signature pattern:
             *      public void Deconstruct(out T1 x1, ..., out Tn xn) { ... }
             * 
             * This uses out parameters instead of tuples, allowing multiple overloads for different numbers of values.
             */
            
            Console.WriteLine("Other (Non-Tuple) Type Deconstruction:");
            PointDeconstruction();
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void LookupName_Old(int id, out string first, out string middle, out string last)
        {
            var person = People[id];
            first = person.First;
            middle = person.Middle;
            last = person.Last;
        }

        private static (string, string, string) LookupName_Tuple(int id)
        {
            var person = People[id];
            return (person.First, person.Middle, person.Last);
        }

        private static (string First, string Middle, string Last) LookupName_TupleNamed(int id)
        {
            var person = People[id];
            return (person.First, person.Middle, person.Last);
        }

        private static void TupleValueEquality()
        {
            // Tuples are value types - their elements are simply public, mutable fields
            // Tuples have value equality!
            // Two tuples are equal if all of their elements are pairwise equal
            var a = (1, "fish", 2, "fish");
            var b = (1, "fish", 2, "fish");

            if (a.Equals(b)) // Note that the "==" operator is not implemented in the NuGet package
            {
                Console.WriteLine($"{a} == {b}");
            }
            else
            {
                Console.WriteLine("Oh no!");
            }
        }

        private static void TupleDictionary()
        {
            // Because Tuples have value equality, equal tuples have the same hash code!
            // This example demonstrates a dictionary with multiple keys!
            var tupleDict = new Dictionary<(int, string), string>
            {
                {(1, "foo"), "bar"},
                {(2, "baz"), "bat"}
            };

            Console.WriteLine($"(1, foo) = {tupleDict[(1, "foo")]}");
            Console.WriteLine($"(2, baz) = {tupleDict[(2, "baz")]}");
        }

        private static void TupleDeconstructingDeclaration()
        {
            (string first, string middle, string last) = LookupName_Tuple(4);
            Console.WriteLine($"{first} {middle} {last}");
        }

        private static void TupleDeconstructingDeclaration_Implicit()
        {
            // Deconstructed variables can be implicitly typed
            (var first, var middle, var last) = LookupName_Tuple(5);
            Console.WriteLine($"{first} {middle} {last}");
        }

        private static void TupleDeconstructingDeclaration_ImplicitAll()
        {
            // Single var for entire deconstructed declaration
            var (first, middle, last) = LookupName_Tuple(6);
            Console.WriteLine($"{first} {middle} {last}");
        }

        private static void TupleDeconstructingAssignment()
        {
            // Existing variables can be assigned to with a "deconstructing assignment"
            var first = "foo";
            var middle = "bar";
            var last = "baz";

            (first, middle, last) = LookupName_Tuple(7);
            Console.WriteLine($"{first} {middle} {last}");
        }

        private static void PointDeconstruction()
        {
            var point = new Point(7, 42);
            var (myx, myy) = point; // Calls Deconstruct(out int x, out int y)
            Console.WriteLine($"({myx}, {myy})");
        }
    }

    public class Person
    {
        public string First;
        public string Middle;
        public string Last;

        public Person(string first, string middle, string last)
        {
            First = first;
            Middle = middle;
            Last = last;
        }
    }

    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) { X = x; Y = y; }
        public void Deconstruct(out int foo, out int bar) { foo = X; bar = Y; }
    }

}
