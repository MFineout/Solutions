using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //When you declare a func type you use generic type parameters to describe the parameters and the return type of a method, and the last generic type parameter in a func always describes the return type of a method, so if I have a func, let's just call it f for right now, and it's a func of int and int, I know I need to work with a method that returns an integer, and then all the other type parameters here are parameters that describe the incoming parameters to the method itself.

            //so func<int, int> passes an int and returns an int. x = int, returns x*x (int)
            //Func<int, int> f = x => x * x;

            Func<int, int> square = x => x * x;
            Console.WriteLine(square(3));

            //this is going to be a method that takes two parameters, and they're both integers. Let's call this add.Now when you have just a single parameter to your lambda expression you do not need parentheses.I can just say x goes to x* x, but when I have 0 or more than 1 parameter I need to put my parameters inside of parentheses, so I need to say, x and y goes to will return x + y,

            //Here you can also say (int x, int y)
            Func<int, int, int> add = (x, y) => x + y;
            Console.WriteLine(square(add(3, 5)));

            //Action has single parameter and returns void
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3, 5)));


            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Matt" },
                new Employee { Id = 2, Name = "Amanda" }
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Ben" }
            };

            //IEnumerator example. Overly complex foreach loop

            /*  Console.WriteLine(developers.Count()); //implementing static method count via MyLinq

              IEnumerator<Employee> enumerator = developers.GetEnumerator();
              while(enumerator.MoveNext())
              {
                  Console.WriteLine(enumerator.Current.Name);
              } */

            foreach (var person in developers.Where(
                   delegate (Employee employee)                // Anonymous method
                   {
                       return employee.Name.StartsWith("M");
                   }))
            {
                Console.WriteLine(person.Name);
            }

            foreach (var person in developers.Where(           // Lambda expression -- succint 
                         e => e.Name.StartsWith("M"))) 
            {
                Console.WriteLine(person.Name);
            }
            //------------------------------------------------------------------------------------------------

            var query = developers.Where(e => e.Name.Length == 5) //Method syntax
                             .OrderBy(e => e.Name);
                          // .Select(e => e);

            var query2 = from developer in developers              //Query syntax
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;
        }
        private static bool NameStartsWithM(Employee employee) //Named Method
        {
            return employee.Name.StartsWith("M");
        }

       
    }
}
