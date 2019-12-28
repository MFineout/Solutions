using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    public static class MyLinq
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source,
                                                    Func<T, bool> predicate)
        {
           // var result = new List<T>();

            foreach(var item in source)
            {
                if(predicate(item))
                {
                    yield return item; // yield return operates on deferred execution 
                   // result.Add(item); --- would have to uncomment result declaration.
                }

                //deferred execution in this example ---
                //The line of code using our filter operator or the LINQ Where operator, you can think of that line of code as defining a query or building a data structure that knows what to do sometime in the future, but the filtering operation doesn't execute until we try to see the results of the query. In our program we do that with a foreach statement. What else  would force a query to execute? Ultimately, any operation that inspects the results will force the query to execute, so if we serialized the query results into JSON or XML
            }

            //return result;
        }
    }
}
