using System;
using System.Globalization;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class FormattableStrings
    {
        void Examples()
        {
            IFormattable christmas = $"{new DateTime(2016, 12, 25):f}";

            /*
            
            // The compiler generates the following code:
            
            IFormattable christmas = FormattableStringFactory.Create("{0:f}", new DateTime(2016, 12, 25));

            //*/

            // The format can be used as follow:
            var christamasText = christmas.ToString(null, new CultureInfo("pt-PT")); // Format xmas as European Portuguese

        }
    }
}


/*

// The features introduced by the C# 6 are compatible with the previous .NET platforms.
// However, this particular feature requires System.Runtime.CompilerServices.FormattableStringFactory
// and System.FormattableString types that have been introduced only in version 4.6 of the platform.

// The good news is that the compiler is not tied to the location of these types in a particular
// assembly and, if we are to use this functionality in an earlier version of the platform, we just
// need to add the implementation of these types.

// The concrete type returned by FormattableStringFactory.Create is derived from:

namespace System
{
    public abstract class FormattableString : IFormattable
    {
        protected FormattableString() { }

        public abstract int ArgumentCount { get; }
        public abstract string Format { get; }
        public static string Invariant(FormattableString formattable) { return null; }
        public abstract object GetArgument(int index);
        public abstract object[] GetArguments();
        public override string ToString() { return null; }
        public abstract string ToString(IFormatProvider formatProvider);
        public abstract string ToString(string str, IFormatProvider formatProvider);
    }
}

//*/
