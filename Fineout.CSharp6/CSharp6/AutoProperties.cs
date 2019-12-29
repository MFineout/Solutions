namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    #region Initializers for Auto-Properties

    public class PersonWithInitializers
    {
        public string First { get; set; } = "Jane"; // Initializer executed at same time as field initializers
        public string Last { get; set; } = "Doe";
    }

    /*
    
    // The above is syntactic sugar which is used to generate the following.
    // Note that the <First>k__BackingField and <Last>k__BackingField have names that are not valid C# names.
    // This is done to ensure that there is no chance of collision between a name that is given by the programmer and a name given by the compiler.
    
    public class PersonWithInitializers
    {
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string <First>k__BackingField = "Jane";
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string <Last>k__BackingField = "Doe";
        
        public string First
        {
            [CompilerGenerated]
            get { return <First>k__BackingField }
            [CompilerGenerated]
            set { <First>k__BackingField = value; }
        }

        public string Last
        {
            [CompilerGenerated]
            get { return <Last>k__BackingField }
            [CompilerGenerated]
            set { <Last>k__BackingField = value; }
        }
    }
    
    //*/

    #endregion Initializers for Auto-Properties

    #region Read-Only Auto-Properties

    public class PersonReadOnly
    {
        public string First { get; } = "Jane"; // Note that ReSharper complains, though this isn't true if we use a non-initializing constructor overload (uncomment below)
        public string Last { get; } = "Doe";

        // As with fields, you can initialize in the constructor
        public PersonReadOnly(string first, string last)
        {
            First = first;
            Last = last;
        }

        //public PersonReadOnly() { }
    }

    /*
    
    // As in the previous case, the generated code will be:

    public class PersonReadOnly
    {
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string <First>k__BackingField = "Jane"; // Note that the backing field is declared readonly
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string <Last>k__BackingField = "Doe";

        public string First
        {
            [CompilerGenerated]
            get { return <First>k__BackingField; }
        }

        public string Last
        {
            [CompilerGenerated]
            get { return <Last>k__BackingField; }
        }

        public Person(string first, string last)
        {
            <First>k__BackingField = first;
            <Last>k__BackingField = last;
        }
    }
    
    //*/

    #endregion Read-Only Auto-Properties
}
