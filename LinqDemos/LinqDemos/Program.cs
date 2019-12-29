using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, OrderId = 1, Email = "a" },
                new Order { CustomerId = 2, OrderId = 2, Email = "a" },
                new Order { CustomerId = 3, OrderId = 3, Email = "b" },
                new Order { CustomerId = 1, OrderId = 4, Email = "b" },
                new Order { CustomerId = 2, OrderId = 5, Email = "b" },
                new Order { CustomerId = 3, OrderId = 6, Email = "b" },
                new Order { CustomerId = 1, OrderId = 7, Email = "b" },
                new Order { CustomerId = 2, OrderId = 8, Email = "b" },
                new Order { CustomerId = 3, OrderId = 9, Email = "b" },
                new Order { CustomerId = 1, OrderId = 10, Email = "b" },
                new Order { CustomerId = 2, OrderId = 11, Email = "b" },
                new Order { CustomerId = 3, OrderId = 12, Email = "b" }
            };

            var ordersByCustomer = orders
                .GroupBy(o => new { o.CustomerId, o.Email, c = (o.CustomerId > 2 ? "new" : "old") });
            //.ThenByDescending(o => o.Key.CustomerId)
            //.Select(o => o.ToList())
            //.ToList();

            var ordersSortedByNewness = ordersByCustomer.OrderBy(o => o.Key.c);
            
            var ordersSortedByEmail = ordersByCustomer.OrderBy(o => o.Key.Email);

            foreach (var customer in ordersByCustomer)
            {
                Console.WriteLine($"CustomerId {customer.Key}");

                foreach (var order in customer)
                {
                    //Console.WriteLine($"\tOrderId {order.OrderId}");
                    Console.WriteLine($"{order}");
                }
            }


            var gen = new ImGeneric<string>("Hi mom!");
            Console.WriteLine($"Type {gen.GetType()} Value {gen.MyValue}");
            var genInt = new ImGeneric<int>(1);
            Console.WriteLine($"Type {genInt.GetType()} Value {genInt.MyValue}");
            var GenOrder = new ImGeneric<Order>(new Order { CustomerId = 1, OrderId = 2 });
            Console.WriteLine($"Type {GenOrder.GetType()} Value {GenOrder.MyValue}");
            
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }


        public class Order
        {
            public int CustomerId { get; set; }
            public int OrderId { get; set; }
            public string Email { get; set; }

            public override string ToString()
            {
                return $"OrderId {OrderId}";
            }
        }

        public class ImGeneric<T>
        {
            private readonly T _input;

            public T MyValue => _input;

            public ImGeneric(T input)
            {
                _input = input;
                
            }
        }
    }
}
