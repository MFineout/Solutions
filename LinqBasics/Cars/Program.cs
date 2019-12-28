using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {

            ///---Expression demonstration ---
            /*
            // important part to understand about LINQ when you're using a technology like the Entity Framework; When you are invoking LINQ operators, and you see that you're working against an IQueryable, and all of the IQueryable methods take an expression of func, the Entity Framework then has the opportunity to inspect your code, and translate it into SQL Statements.

            Func<int, int> Square = x => x * x;
            //the C# Compiler no longer compiles this code (expression) into something that I can execute or something that I can invoke. Instead, what the C# Compiler will do is give me something that is a data structure that represents the code. 
            Expression<Func<int, int, int>> Add = (x, y) => x + y;

            Func<int, int, int> addI = Add.Compile();

            var result = addI(3, 5);
            //Console.WriteLine(result);
            Console.WriteLine(Add);
            /// */



            //if car database in memory doesn't match car definition, drop and recreate a new CarDb from scratch
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());


            InsertData();
            QueryData();

        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine; //logs the database log to the console -- a way to check that the information is going to the database without checking the database itself

            //----query syntax
            var query =
                from car in db.Cars
                orderby car.Combined descending, car.Name ascending
                select car;

            //---extension method syntax

            //Note that EF will only work with IQueryable types. If the method calls for IEnumerables, it must be done in memory.
            /* var queryEx =
                 db.Cars.Where(c => c.Manufacturer == "BMW")
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name)
                        // .Take(10)
                        // .Select(c => new { Name = c.Name.Split(" ") })
                        // .ToList();
                        // note how this doesn't work because EF can not understand Split and this works as an IQueryable 
                        .Take(10)
                        .ToList()
                        .Select(c => new { Name = c.Name.Split(' ') }); //here we are working in memory and can use methods such as split

             foreach (var car in queryEx)
             {
                 for (var i = 0; i < car.Name.Length; i++)
                 {
                     Console.WriteLine($"{car.Name[i]}");
                 }
             } */

            //-- more complex syntax for EF using Ex. Methods

            var query2 =
                db.Cars.GroupBy(c => c.Manufacturer)
                       .Select(g => new
                       {
                           Name = g.Key,
                           Cars = g.OrderByDescending(c => c.Combined).Take(2)
                       });


            //-- executing same code using query syntax

            var query3 =
                from car in db.Cars
                group car by car.Manufacturer into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer //can even use query syntax within anon constructor.
                            orderby car.Combined descending
                            select car).Take(2)
                };

            foreach(var group in query3)
            {
                Console.WriteLine(group.Name);
                foreach(var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }


        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb(); //Entity Framework will assume that I want to connect to a database that has the same name as my DbContext derived class, including the namespace, so in this case, if I instantiate CarDb the Entity Framework will try to connect to a Cars.CarDb database on the localdb instance on this machine.

            db.Database.Log = Console.WriteLine;


            if (!db.Cars.Any())
            {
                foreach(var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }

        }

            private static void QueryXml()
        {
            var document = XDocument.Load("fuel.xml");

            var ns = (XNamespace)"HTTP://pluralsight.com/cars/2016";
            var ex = (XNamespace)"HTTP://pluralsight.com/cars/2016/ex";

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car") //searches elements of the child "Car" element
                                            ?? Enumerable.Empty<XElement>() // if a null value is found return empty enumerable instead of null reference exception
               // from element in document.Descendants("Car")
                where element.Attribute("Manufacturer")?.Value == "BMW" //matches elements where "Man." == "BMW"
                select element.Attribute("Name").Value; //selects those elements by name

            foreach(var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"HTTP://pluralsight.com/cars/2016";
            var ex = (XNamespace)"HTTP://pluralsight.com/cars/2016/ex";

            var document = new XDocument();
            var cars = new XElement("Cars");

            var smlCars = new XElement(ns + "Cars", //puts cars in the namespace above (pluralsight)
                 from record in records
                 select new XElement(ex + "Car", //projects (maps) the properties desired to new element "Cars"
                        new XAttribute("Combined", record.Combined),
                        new XAttribute("Name", record.Name),
                        new XAttribute("Manufacturer", record.Manufacturer))
                );

            smlCars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex)); //adds prefix namespace to xml

            document.Add(smlCars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCars(string path)
        {
            /* return File.ReadAllLines(path) //reads all lines in csv
                        .Skip(1) //skips header line (first line of csv) -- using skip and take you can parse out the middle of a file. I.E. skip 2, take 8.. gets lines 3-10 of a file
                        .Where(line => line.Length > 1)
                        .Select(Car.ParseFromCsv)
                        .ToList(); */

            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();

        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                   .Where(l => l.Length > 1)
                   .Select(l =>
                   {
                       var columns = l.Split(",");
                       return new Manufacturer
                       {
                           Name = columns[0],
                           Headquarters = columns[1],
                           Year = int.Parse(columns[2])
                       };
                   });
            return query.ToList();
        }

    }

    public class CarStatistics //accumulator class for aggregate method
    {
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;

            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var columns = line.Split(",");

                yield return new Car //deferred execution, acts as select operator. Projection.
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
