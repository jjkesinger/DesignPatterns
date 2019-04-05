using System;
using System.Threading.Tasks;

namespace Insperity.Integration.Trucking.Core
{
    public interface ILogger
    {
        Task LogError(Exception e);
        Task LogMessage(object message);
    }
}
