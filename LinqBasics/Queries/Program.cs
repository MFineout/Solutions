using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>()
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca", Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980 }
            };


            //--- essentially, until execution, linq methods are just building a data structure.
            var query = movies.Filter(m => m.Year > 2000);

            
            var query2 = query.Take(1);

            Console.WriteLine("Initial linq method iteration.");
            foreach(var movie in query)
            {
                Console.WriteLine(movie.Title);
            }

            // this execution of query 2 could be in much different section of code even though it was defined in base layer.
            Console.WriteLine("With added on linq method later in code.");
            foreach(var movie in query2)
            {
                Console.WriteLine(movie.Title);
            }


            //If you have to work with the filtered data throughout the program, instead of declaring just the filter method, you can put the filtered results into a concrete result such as a list or array. Then when you call on that data to select just 5 or 10 results, the data won't have to be filtered again, you can just perform the operation on the already filtered data.
            //If you have a list of 10,000 items to filter through, this is much more efficient.
            var query3 = movies.Filter(m => m.Year > 2000).ToList();

            Console.WriteLine($"Query 3: {query3.Count}");

            var query4 = query.Take(1);

            Console.WriteLine("Initial linq method iteration.");
            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }


        }
    }
}
