using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Core;

namespace Insperity.Integration.Trucking.Test.Fakes
{
    public class FakeLogger : ILogger
    {
        public static ConcurrentDictionary<object, int> Dictionary = new ConcurrentDictionary<object, int>();
        public async Task LogError(Exception e)
        {
            await Task.CompletedTask;
        }

        public async Task LogMessage(object message)
        {
            Dictionary.AddOrUpdate(message, (f) => 1, (obj, i) =>
            {
                var cnt = Dictionary[obj];
                cnt++;
                return cnt;
            });

            await Task.CompletedTask;
        }
    }
}
