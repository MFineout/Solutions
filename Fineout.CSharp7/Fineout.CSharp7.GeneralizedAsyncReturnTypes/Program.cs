using System;
using System.Threading.Tasks;

namespace Fineout.CSharp7.GeneralizedAsyncReturnTypes
{
    // See: https://docs.microsoft.com/en-us/dotnet/articles/csharp/whats-new/csharp-7#generalized-async-return-types

    /*
     * Generalized Async Return Types
     * 
     * Returning a Task object from async methods can introduce performance bottlenecks in certain paths.
     * Task is a reference type, so using it means allocating an object. In cases where a method declared
     * with the async modifier returns a cached result, or completes synchronously, the extra allocations
     * can become a significant time cost in performance critical sections of code. It can become very
     * costly if those allocations occur in tight loops.
     * 
     * The new language feature means that async methods may return other types in addition to Task,
     * Task<T> and void. The returned type must still satisfy the async pattern, meaning a GetAwaiter
     * method must be accessible. As one concrete example, the ValueTask type has been added to the
     * .NET framework to make use of this new language feature.
     * 
     * 
     * Note that If you're targeting .NET 4.6.2 or lower or .NET Core,
     * you need to install the NuGet package System.Threading.Tasks.Extensions:
     *      Install-Package "System.Threading.Tasks.Extensions"
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Async with ValueTask:");
            var i = await Func();
            // Note that i is of type int!
            Console.WriteLine($"Result: {i}");
            Console.WriteLine($"Type: {i.GetType()}");
            Console.WriteLine();

            Console.WriteLine("Async with ValueTask - Cached Example:");
            var x = await CachedFunc();
            Console.WriteLine($"Result (First Call): {x}");
            x = await CachedFunc();
            Console.WriteLine($"Result (Second Call): {x}");
            Console.WriteLine();


            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static async ValueTask<int> Func()
        {
            // Simulate async work:
            await Task.Delay(100);
            return 5;
        }

        private static ValueTask<int> CachedFunc()
        {
            return (_cache) ? new ValueTask<int>(_cacheResult) : new ValueTask<int>(LoadCache());
        }
        private static bool _cache;
        private static int _cacheResult;
        private static async Task<int> LoadCache()
        {
            // Simulate async work:
            await Task.Delay(3000);
            _cacheResult = 100;
            _cache = true;
            return _cacheResult;
        }
    }
}
