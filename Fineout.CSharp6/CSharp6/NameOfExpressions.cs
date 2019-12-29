using System;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class NameOfExpressions
    {
        void Examples(int? x, Person person)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            var s = nameof(person.Age);

            /*
            
            // This is translated to:

            if (x == null) throw new ArgumentNullException("x");
            var s = "Age";

            //*/


            // NOTE: It is not allowed to use primitive types (int, long, char, bool, string, etc.) in nameof
            // expressions because they are not expressions and the argument of nameof is an expression.

            //nameof(9); // bad!
            //nameof("Aloha World!"); // still bad!
        }
    }
}
