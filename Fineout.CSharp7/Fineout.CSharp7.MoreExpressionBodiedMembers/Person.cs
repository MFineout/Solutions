using System.Collections.Concurrent;

namespace Fineout.CSharp7.MoreExpressionBodiedMembers
{
    // See: https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/
    
    public class Person
    {
        private static ConcurrentDictionary<int, string> names = new ConcurrentDictionary<int, string>();
        private int _id;

        public Person(string name) => names.TryAdd(_id, name); // constructors
        ~Person() => names.TryRemove(_id, out _);              // finalizers

        public string OldExample => "something"; // C# 6 - accessor only, no mutator available

        public string Name
        {
            get => names[_id];                                 // getters
            set => names[_id] = value;                         // setters
        }
    }
}
