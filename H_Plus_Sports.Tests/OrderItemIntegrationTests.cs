using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace H_Plus_Sports.Tests
{
    [TestClass]
    public class OrderItemIntegrationTests
    {
        private readonly HttpClient _client;

        public OrderItemIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void OrderItemGetAllTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/OrderItems/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1001)]
        public void OrderItemGetOneTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/OrderItems/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void OrderItemPostTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"/api/OrderItems/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OrderItemPutTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/OrderItems/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OrderItemDeleteTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"/api/OrderItems/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
