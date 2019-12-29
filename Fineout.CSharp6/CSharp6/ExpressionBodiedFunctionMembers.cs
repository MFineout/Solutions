using System;
using System.Collections.Generic;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    #region Expression Bodied Method-Like Members

    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Expression bodied method-like member
        public Point Move(int dx, int dy) => new Point(X + dx, Y + dy);
    }

    public class Complex
    {
        private readonly int _val;

        public Complex(int initial)
        {
            _val = initial;
        }

        public Complex Add(Complex add)
        {
            return new Complex(_val + add._val);
        }

        // Expression bodied explicit operator
        public static Complex operator +(Complex a, Complex b) => a.Add(b);
    }

    public class Person
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public List<int> Orders { get; set; }

        // Expression bodied implicit operator
        public static implicit operator string(Person p) => p.First + " " + p.Last;

        // For methods whose return type is void (or Task for asynchronous methods), the lambda arrow (=>) syntax still applies,
        // but the subsequent expression must be a statement (this is similar to what already happens with lambdas):
        public void Print() => Console.WriteLine(this); // In turn, this will execute our implicit string operator

        /*
        
        // The compiler actually translates the Print() method as follows:

        public void Print()
        {
            Console.WriteLine(First + " " + Last);
        }

        //*/


        #region Expression Bodied Property-Like Function Members

        #region Helper Stuff

        public class PersonStore
        {
            public Person LookupPerson(long id)
            {
                return new Person();
            }
        }

        public PersonStore Store;

        #endregion Helper Stuff

        public string Name => First + " " + Last;

        public Person this[long id] => Store.LookupPerson(id);

        /*
        
        // The previous examples are translated by the compiler to:

        public string Name
        {
            get
            {
                return First + " " + Last;
            }
        }
        public Person this[long id]
        {
            get
            {
                return Store.LookupPerson(id);
            }
        }

        //*/

        #endregion Expression Bodied Property-Like Function Members

    }

    #endregion Expression Bodied Method-Like Members
}
