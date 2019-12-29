using static System.Linq.Enumerable; // The type, not the namespace

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class Program2
    {
        /*
        
        Extension methods are invoked like regular static methods, (See: https://msdn.microsoft.com/en-gb/magazine/dn879355.aspx)
        but they are called as if they were instance methods on the extended type.
        Instead of bringing these methods to the current scope, the static import functionality makes these methods available
        as extension methods without the need to import all extension methods in a namespace like before:
        
        */

        static void Main()
        {
            var range = Range(5, 17);                // Ok: not extension
            //var odd = Where(range, i => i % 2 == 1); // Error, not in scope
            var even = range.Where(i => i % 2 == 0); // Ok
        }
    }
}
