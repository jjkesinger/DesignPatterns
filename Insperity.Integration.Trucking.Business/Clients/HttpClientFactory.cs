using System.Net.Http;

namespace Insperity.Integration.Trucking.Business.Clients
{
    //Factory pattern
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }

    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
