using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Insperity.Integration.Trucking.Business.Clients;
using Insperity.Integration.Trucking.Business.Model;
using Insperity.Integration.Trucking.Business.Serialization;
using Insperity.Integration.Trucking.Core.Configuration.Http;
using Insperity.Integration.Trucking.Core.Utility;
using Insperity.Integration.Trucking.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Insperity.Integration.Trucking.Test.Business.Client
{
    [TestClass]
    public class HttpWriteClientTests
    {
        private readonly Employee _employee = new Employee(
            new Company(1, "", new List<EldProvider>(){new GeotabEldProvider("a", "a", "a")}), 1, "John", "Kesinger", "Jkesinger");

        private readonly Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private readonly IHttpClientFactory _httpClient;
        public HttpWriteClientTests()
        {
            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler>(){ CallBase = true };
            _httpClient = new FakeHttpClientFactory(_fakeHttpMessageHandler.Object);
        }

        private class TestConfiguration : HttpConfiguration
        {
            public override string BaseUrl => @"http:\\test.com";
            public override string AddUrl => "Add";
            public override string DeleteUrl => "Delete";
            public override string UpdateUrl => "Update";
        }

        private class EmployeeHttpWriteClientImplementation : HttpWriteClient<Employee>
        {
            public EmployeeHttpWriteClientImplementation(IHttpClientFactory httpClientFactory, HttpConfiguration configuration, ClientSerializer<Employee> serializer = null) 
                : base(httpClientFactory, configuration, serializer)
            {
                
            }
        }

        [TestMethod]
        public async Task AddEntityShouldSuccessfullySubmitHttpPostRequest()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Add(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [TestMethod]
        public async Task AddEntityShouldSuccessfullySubmitHttpPostRequestApplicationXml()
        {
            //Arrange
            var actual = string.Empty;
            var actualMediaType = string.Empty;
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(async (obj) =>
                {
                    actual = await obj.Content.ReadAsStringAsync();
                    actualMediaType = obj.Content.Headers.ContentType.ToString();
                })
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config, new ApplicationXmlSerializer<Employee>());
            var expected = XmlSerializer<Employee>.Serialize(_employee);

            //Act
            await implementation.Add(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actualMediaType.Contains("application/xml"));
        }

        [TestMethod]
        public async Task AddEntityShouldSuccessfullySubmitHttpPostRequestTextXml()
        {
            //Arrange
            var actual = string.Empty;
            var actualMediaType = string.Empty;
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(async (obj) =>
                {
                    actual = await obj.Content.ReadAsStringAsync();
                    actualMediaType = obj.Content.Headers.ContentType.ToString();
                })
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config, new TextXmlSerializer<Employee>());
            var expected = XmlSerializer<Employee>.Serialize(_employee);

            //Act
            await implementation.Add(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actualMediaType.Contains("text/xml"));
        }

        [TestMethod]
        public async Task AddEntityShouldSuccessfullySubmitHttpPostRequestJson()
        {
            //Arrange
            var actual = string.Empty;
            var actualMediaType = string.Empty;
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(async (obj) =>
                {
                    actual = await obj.Content.ReadAsStringAsync();
                    actualMediaType = obj.Content.Headers.ContentType.ToString();
                })
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);
            var expected = JsonConvert.SerializeObject(_employee);

            //Act
            await implementation.Add(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actualMediaType.Contains("application/json"));
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task AddEntityWithInvalidAuthenticationShouldThrow()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Add(_employee);

            //Assert
            //throw exception
        }

        [TestMethod]
        public async Task UpdateEntityShouldSuccessfullySubmitHttpPostRequest()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Update(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task UpdateEntityWithInvalidAuthenticationShouldThrow()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Update(_employee);

            //Assert
            //throw exception
        }

        [TestMethod]
        public async Task DeleteEntityShouldSuccessfullySubmitHttpPostRequest()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Delete(_employee);

            //Assert
            _fakeHttpMessageHandler.Verify(f => f.Send(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task DeleteEntityWithInvalidAuthenticationShouldThrow()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(string.Empty)
                });

            var config = new TestConfiguration();
            var implementation = new EmployeeHttpWriteClientImplementation(_httpClient, config);

            //Act
            await implementation.Delete(_employee);

            //Assert
            //throw exception
        }
    }
}
