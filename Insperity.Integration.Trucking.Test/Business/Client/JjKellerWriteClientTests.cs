using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Clients.JJKeller;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Core.Configuration;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Insperity.Integration.Trucking.Test.Business.Client
{
    [TestClass]
    public class JjKellerWriteClientTests
    {
        private readonly Employee _employee = new Employee(
            new Company(1, "", new List<EldProvider>() { new JjKellerEldProvider("aasdfdsf") }), 1, "John", "Kesinger", "Jkesinger");

        private readonly Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private readonly IHttpClientFactory _httpClient;
        public JjKellerWriteClientTests()
        {
            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler>() { CallBase = true };
            _httpClient = new FakeHttpClientFactory(_fakeHttpMessageHandler.Object);
        }

        [TestMethod]
        public void ShouldSetApiKeyHeader()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new JjKellerHttpConfiguration();
            var implementation = new JjKellerHttpWriteClient<Employee>(_httpClient, new EmployeeConfiguration(config));

            //Act
            implementation.SetApiKey("1234");

            //Assert
            Assert.IsTrue(implementation.HttpClient.DefaultRequestHeaders.Contains("Keller-API-Key"));
            Assert.IsTrue(implementation.HttpClient.DefaultRequestHeaders.First(f => f.Key == "Keller-API-Key").Value.Contains("1234"));
        }
    }
}
