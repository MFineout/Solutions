using static System.Console;
using static System.DayOfWeek;
using static System.Math;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class Program
    {
        static void Main()
        {
            WriteLine(Sqrt(3 * 3 + 4 * 4));
            WriteLine(Friday - Monday);
        }
    }

    /*
    
    // The previous code is translated by the compiler to:

    class Program
    {
        static void Main()
        {
            System.Console.WriteLine(System.Math.Sqrt(3 * 3 + 4 * 4));
            System.Console.WriteLine(System.DayOfWeek.Friday - System.DayOfWeek.Monday);
        }
    }
    
    //*/
}
