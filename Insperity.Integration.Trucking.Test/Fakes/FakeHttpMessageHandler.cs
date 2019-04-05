using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Clients;

namespace Insperity.Integration.Trucking.Test.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {

        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("Now we can setup this method with our mocking framework");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Thread.Sleep(300);
            return Task.FromResult(Send(request));
        }
    }

    public class FakeHttpClient : HttpClient
    {
        public FakeHttpClient() : base(new FakeHttpMessageHandler())
        {
            
        }
    }

    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpMessageHandler _handler;
        public FakeHttpClientFactory(HttpMessageHandler handler)
        {
            _handler = handler;
        }

        public HttpClient CreateClient()
        {
            return new HttpClient(_handler);
        }
    }
}
