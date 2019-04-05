using System;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Core.Utility;

namespace Insperity.Integration.Trucking.Business.Clients
{
    public abstract class HttpWriteClient<T> : IWriteClient<T> where T: ISerializable
    {
        private readonly ClientSerializer<T> _serializer;
        public readonly HttpClient HttpClient;
        public string AddUrl { get; protected set; }
        public string UpdateUrl { get; protected set; }
        public string DeleteUrl { get; protected set; }
        public string BaseUrl { get; protected set; }

        protected HttpWriteClient(IHttpClientFactory httpClientFactory,
            HttpConfiguration configuration,
            ClientSerializer<T> serializer)
        {
            _serializer = serializer ?? new JsonSerializer<T>();

            HttpClient = httpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri(configuration.BaseUrl);

            AddUrl = configuration.AddUrl;
            UpdateUrl = configuration.UpdateUrl;
            DeleteUrl = configuration.DeleteUrl;
            BaseUrl = configuration.BaseUrl;   
        }

        public virtual async Task Add(T entity)
        {
#if !DEBUG
            var response = await HttpClient.PostAsync(AddUrl, GetHttpContent(entity));
            response.EnsureSuccessStatusCode();
#else
            Thread.Sleep(300);
            await Task.CompletedTask;
#endif
        }

        public virtual async Task Update(T entity)
        {
#if !DEBUG
            var response = await HttpClient.PostAsync(UpdateUrl, GetHttpContent(entity));
            response.EnsureSuccessStatusCode();
#else
            Thread.Sleep(300);
            await Task.CompletedTask;
#endif
        }

        public virtual async Task Delete(T entity)
        {
#if !DEBUG
            var response = await HttpClient.DeleteAsync(GetDeleteUrl(entity.Id.ToString()));
            response.EnsureSuccessStatusCode();
#else
            Thread.Sleep(300);
            await Task.CompletedTask;
#endif
        }

        private HttpContent GetHttpContent(T entity)
        {
            //using the strategy pattern to eliminate need for switch statement
            //base on serialization used.
            return new StringContent(_serializer.Serialize(entity), _serializer.Encoding, _serializer.MediaType);
        }

        private string GetDeleteUrl(string id)
        {
            return Path.Combine(DeleteUrl, id);
        }
    }
}
