using System;
using System.Collections.Generic;
using System.Text;

namespace Features
{
    public static class MyLinq
    {
        //for an extension method to be availale, the namespace where the extension method lives has to be in effect.
        //or simply using.(namespace) it will be allowed

        public static int Count<T>(this IEnumerable<T> sequence) //creates extension method for any IEnumerable<T> to implement static method Count
        { 
            int count = 0;
            foreach(var item in sequence)
            {
                count++;
            }
            return count;
        }

    }
}
