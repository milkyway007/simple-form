using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Tests
{
    [TestFixture]
    public class SectorOptionsControllerTests
    {
        public const string SECTOR_OPTIONS_GET_URL = "/api/sectorOptions";
        public const string CONTENT_TYPE = "application/json; charset=utf-8";
        public const int SECTOR_OPTIONS_COUNT = 3;
        private WebApplicationFactory<Program> _application;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _application.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task Get_EndpointShouldReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync(SECTOR_OPTIONS_GET_URL);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Get_EndpointShouldReturnCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync(SECTOR_OPTIONS_GET_URL);

            // Assert
            Assert.AreEqual(CONTENT_TYPE, response.Content.Headers.ContentType.ToString());
        }

        [Test]
        public async Task Get_EndpointShouldReturnSectorOptions()
        {
            // Act
            var response = await _client.GetAsync(SECTOR_OPTIONS_GET_URL);
            var responseBody = await response.Content.ReadAsStringAsync();
            var sectorOptions = JsonConvert.DeserializeObject<List<SectorOption>>(responseBody);

            // Assert
            Assert.AreEqual(SECTOR_OPTIONS_COUNT, sectorOptions.Count);
            Assert.True(sectorOptions.All(x => x.Level == 1));
            Assert.True(sectorOptions.All(x => x.Children.Count() > 0));
        }
    }
}
