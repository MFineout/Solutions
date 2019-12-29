using System;

namespace Fineout.CSharp7.MoreExpressionBodiedMembers
{
    // See: https://docs.microsoft.com/en-us/dotnet/articles/csharp/whats-new/csharp-7#more-expression-bodied-members

    public class ExpressionMembersExample
    {
        // Expression-bodied constructor
        public ExpressionMembersExample(string label) => this.Label = label;

        // Expression-bodied finalizer
        ~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");

        private string label;

        // Expression-bodied get accessor / set mutator
        public string Label
        {
            get => label;
            set => this.label = value ?? "Default label";
        }
    }
}
