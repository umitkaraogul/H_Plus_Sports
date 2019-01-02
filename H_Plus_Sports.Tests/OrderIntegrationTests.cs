using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H_Plus_Sports.Tests
{
    [TestClass]
    public class OrderIntegrationTests
    {
        private readonly HttpClient _client;
        public OrderIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void OrderGetAllTest()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Orders/");
            //Act
            var response = _client.SendAsync(request).Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1001)]
        public void OrderGetOneTest(int id)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Orders/{id}");
            //Act
            var response = _client.SendAsync(request).Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void OrderPostTest()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/Orders/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OrderPutTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Orders/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OrderDeleteTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Orders/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}