using System;

namespace Fineout.CSharp7.LiteralImprovements
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Digit Separator:");
            DigitSeparator();
            Console.WriteLine();

            Console.WriteLine("Binary Literals:");
            BinaryLiterals();
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void DigitSeparator()
        {
            // Can now use _ (underscore) as a digit separator inside number literals:
            var d = 123456;
            var ds = 123_456;
            if (d == ds)
            {
                Console.WriteLine("123456 == 123_456");
            }
            else
            {
                Console.WriteLine("Inconceivable!");
            }

            var x = 0xABCDEF;
            var xs = 0xAB_CD_EF;
            if (x == xs)
            {
                Console.WriteLine("0xABCDEF == 0xAB_CD_EF");
            }
            else
            {
                Console.WriteLine("Inconceivable!");
            }
        }

        private static void BinaryLiterals()
        {
            // Introducing binary literals to specify bit patterns directly (ex. instead of hex notation)
            var b = 0b1010_1011_1100_1101_1110_1111;
            Console.WriteLine($"0b1010_1011_1100_1101_1110_1111 = {b}");

            var xs = 0xAB_CD_EF;
            if (b == xs)
            {
                Console.WriteLine("0b1010_1011_1100_1101_1110_1111 == 0xAB_CD_EF");
            }
            else
            {
                Console.WriteLine("Inconceivable!");
            }
        }
    }
}
