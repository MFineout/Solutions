namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    // The names used by the compiler are the source names and not the metadata names of the artifacts.

    using S = System.String;
    class C
    {
        void M<T>(S s)
        {
            var s1 = nameof(T);
            var s2 = nameof(S);
        }
    }

    /*
    
    // This is converted by the compiler to:

    using S = System.String;
    class C
    {
        void M<T>(S s)
        {
            var s1 = "T"; // NOT the name of the Type T
            var s2 = "S"; // NOT String
        }
    }
    
    //*/
}
