namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class StringInterpolation
    {
        void Examples(Person p)
        {
            // The old way:
            // ReSharper disable once UseStringInterpolation - well, that's what I'm demonstrating :)
            var s = string.Format("{0} is {1} year{{s}} old.", p.Name, p.Age);

            // The interpolation syntax allows strings to directly replace the literal string indexes
            // by "holes" with the expressions that correspond to the values:
            var s2 = $"{p.Name} is {p.Age} year{{s}} old.";

            // As with the string.Format method, it is possible to specify shapes and alignments:
            var s3 = $"{p.Name,20} is {p.Age:D3} year{{s}} old.";

            // The content of the holes can be any expression, including strings:
            var s4 = $"{p.Name} is {p.Age} year{(p.Age == 1 ? "" : "s")} old.";
            
            // Note that the conditional expression is in brackets, so that: "s" is not confused with the format specifier.
        }
    }
}
