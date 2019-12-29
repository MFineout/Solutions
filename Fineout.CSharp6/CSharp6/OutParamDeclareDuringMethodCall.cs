namespace CSharp6
{
    // See http://www.developer.com/net/csharp/slideshows/top-10-csharp-6.0-language-features.html

    class OutParamDeclareDuringMethodCall
    {
        /*
        
        // NOTE: This was dropped from C# 6.0 but pending release in a subsequent update
        // See http://odetocode.com/blogs/scott/archive/2014/09/15/c-6-0-features-part-3-declaration-expressions.aspx

        public bool ConvertToIntegerAndCheckForGreaterThan10(string value)
        {
            if (int.TryParse(value, out int convertedValue)
                && convertedValue > 10)
            {
                return true;
            }

            return false;
        }
        
        //*/
    }
}
