using System.Collections.Generic;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class IndexInitializers
    {
        void Examples()
        {
            // We can finally use initializers that use indexes!
            var numbers = new Dictionary<int, string>
            {
                [7] = "seven",
                [9] = "nine",
                [13] = "really big twelve"
            };

            /*
            
            // This will be translated into:
            
            var dictionary = new Dictionary<int, string>();
            dictionary[7] = "seven";
            dictionary[9] = "nine";
            dictionary[13] = "really big twelve";
            var numbers = dictionary;

            //*/
        }
    }
}
