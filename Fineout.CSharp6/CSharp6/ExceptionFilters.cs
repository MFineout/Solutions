using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CSharp6
{
    // SEE https://www.simple-talk.com/dotnet/.net-framework/whats-new-in-c-6/

    class ExceptionFilters
    {
        void Examples()
        {
            // Exception filters, a CLR feature already provided by Visual Basic and F#, will now also be available in C#:
            try
            {
                // ...
            }
            catch (Exception ex) when (SomeFilter(ex))
            {
                // If SomeFilter(ex) returns null, the catch block is ignored
            }


            // This allows filters to be laid over a catch block for the same type of exception:
            try
            {
                // ...
            }
            catch (SqlException ex) when (ex.Number == 2)
            {
                // ONLY Executes when ex.Number is 2
            }
            catch (SqlException ex)
            {
                // ONLY Executes when ex.Number is not 2
            }
        }

        bool SomeFilter(Exception ex)
        {
            return ex is ArgumentNullException || ex is IndexOutOfRangeException;
        }

        #region 'await' in 'catch' and 'finally' blocks

        public class Resource
        {
            public static async Task<Resource> OpenAsync()
            {
                return new Resource();
            }

            public static async Task LogAsync(Resource res, Exception e)
            {
                // ...
            }

            public async Task CloseAsync()
            {
                // ...
            }
        }

        async void AsyncExamples()
        {
            Resource res = null;
            try
            {
                res = await Resource.OpenAsync();

            }
            catch (Exception e)
            {
                await Resource.LogAsync(res, e);
            }
            finally
            {
                if (res != null) await res.CloseAsync();
            }
        }

        #endregion 'await' in 'catch' and 'finally' blocks
    }
}
