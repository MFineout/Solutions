using System.Linq;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class NullConditionalOperator
    {
        private readonly Person[] _people;

        public NullConditionalOperator(Person[] people)
        {
            _people = people;
        }

        void Examples()
        {
            // Operators with null checks allow access to members and elements only when the receiver is not null, otherwise returning a null result:
            int? length = _people?.Length; // null if _people is null
            Person first = _people?[0];    // null if _people is null

            /*
            
            // The above code is translated into:
            
            int? length = (_people != null) ? new int?(_people.Length) : null;
            Person first = (_people != null) ? _people[0] : null;
            
            //*/


            // The null - conditional operators can be very convenient when used with the coalesce operator (??):
            int length2 = _people?.Length ?? 0; // 0 if _people is null


            // The null-conditional operators have a short-circuit behaviour. In the chain of access to members,
            // elements or invocations immediately adjacent will only run if the original recipient is not null:
            int? first2 = _people?[0].Orders.Count(); // Orders and Count never invoke if _people is null

            /*

            // This example is, in essence, equivalent to:

            int? first2 = (_people != null) ? _people[0].Orders.Count() : (int?) null;
            
            // ... except that _people is evaluated only once!

            //*/


            // Nothing prevents null - conditional operators from being chained, if null verification is required more than once in the chain:
            int? first3 = _people?[0]?.Orders.Count(); // null if _people is null OR _people[0] is null
            
        }

        #region Delegate Invocation

        /*
        
        // In an invocation, a list of arguments in parentheses cannot be immediately preceded by the ‘?’ operator.
        // It would lead to too many ambiguities. Therefore, the invoking of a delegate that one might expect if it
        // is not null is not allowed.
        
        // However, the delegate can always be invoked via its Invoke method:
        
        if (predicate?.Invoke(e) ?? false) { … } // Will only invoke if predicate is not null, else null will coalesce to false
        

        // A common use of this feature is the event trigger:

        PropertyChanged?.Invoke(this, args);

        // ... which translates to:

        var handler = PropertyChanged;
        if (handler != null)
        {
            handler.Invoke(this, args);
        }

        //*/

        #endregion Delegate Invokation
    }
}
