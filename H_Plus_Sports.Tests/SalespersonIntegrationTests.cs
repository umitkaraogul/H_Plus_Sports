using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H_Plus_Sports.Tests
{
    [TestClass]
    public class SalespersonIntegrationTests
    {
        private readonly HttpClient _client;

        public SalespersonIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void SalespersonGetAllTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Salespersons/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(101)]
        public void SalespersonGetOneTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Salespersons/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void SalespersonPostTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"/api/Salespersons/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void SalespersonPutTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Salespersons/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void SalespersonDeleteTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"/api/Salespersons/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
