using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fineout.CSharp7.RefReturnsAndLocals
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

    /*
     * Ref Returns and Locals
     * 
     * There are some restrictions to ensure that this is safe:
     *  (1) You can only return refs that are "safe to return":
     *      Ones that were passed to you, and ones that point into fields in objects.
     *  (2) Ref locals are initialized to a certain storage location,
     *      and cannot be mutated to point to another.
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ref Returns and Local Variables:");
            int[] array = { 1, 15, -39, 0, 7, 14, -12 };
            Console.WriteLine($"Position 4 before: {array[4]}");

            // Ref local variable!
            ref int place = ref Find(7, array); // aliases 7's place in the array
            place = 9; // replaces 7 with 9 in the array

            Console.WriteLine($"Position 4 after: {array[4]}");
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        // Can now return by reference with the "ref" modifier
        private static ref int Find(int number, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                {
                    // Return location in the array by reference, not the value
                    return ref numbers[i];
                }
            }

            throw new IndexOutOfRangeException($"{nameof(number)} not found");
        }
    }
}
