using Application.Users;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Persistence;
using Persistence.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        public const string USERS_CONTROLLER_URL = "/api/users";
        public const string CONTENT_TYPE = "application/json";
        public const string CONTENT_TYPE_CHARSET = "application/json; charset=utf-8";
        private WebApplicationFactory<Program> _application;
        private HttpClient _client;
        private StringContent _userDtoJsonToCreate;
        private StringContent _userDtoJsonToUpdate;
        private Guid _userId;
        private IQueryable<Guid> _userSectorOptionIdsToCreate;
        private IQueryable<Guid> _userSectorOptionIdsToUpdate;
        private IDataContext _context;

        [SetUp]
        public void SetUp()
        {
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();

            var scope = _application.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<DataContext>();
            _userId = Guid.NewGuid();
            _userSectorOptionIdsToCreate = _context.SectorOptions.AsQueryable()
                    .Take(3)
                    .Select(x => x.Id);
            _userSectorOptionIdsToUpdate = _context.SectorOptions.AsQueryable()
                    .Skip(3)
                    .Take(3)
                    .Select(x => x.Id);

            var userDtoToCreate = new UserDto
            {
                Id = _userId,
                Name = "Fake Name",
                SectorOptionIds = _userSectorOptionIdsToCreate,
            };
            _userDtoJsonToCreate = new StringContent(
                JsonConvert.SerializeObject(userDtoToCreate, Formatting.Indented),
                Encoding.UTF8,
                CONTENT_TYPE);

            var userDtoToUpdate = new UserDto
            {
                Id = _userId,
                Name = "Fake Name",
                SectorOptionIds = _userSectorOptionIdsToUpdate,
            };
            _userDtoJsonToUpdate = new StringContent(
                JsonConvert.SerializeObject(userDtoToUpdate, Formatting.Indented),
                Encoding.UTF8,
                CONTENT_TYPE);
        }

        [TearDown]
        public void TearDown()
        {
            _application.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task Put_NewUser_EndpointShouldReturnSuccess()
        {
            // Act
            var response = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Put_NewUser_EndpointShouldReturnCorrectContentType()
        {
            // Act
            var response = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);

            // Assert
            Assert.AreEqual(CONTENT_TYPE_CHARSET, response.Content.Headers.ContentType.ToString());
        }

        [Test]
        public async Task Put_NewUser_EndpointShouldCreateUserSectorOptions()
        {
            // Act
            var response = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var userSectorOptions = _context.UserSectorOptons.Where(x => x.UserId == _userId).ToList();

            // Assert
            Assert.AreEqual(3, userSectorOptions.Count);
            CollectionAssert.AreEqual(_userSectorOptionIdsToCreate, userSectorOptions.Select(x => x.SectorOptionId));
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldReturnSuccess()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToUpdate);

            // Assert
            responseToUpdate.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldReturnCorrectContentType()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToUpdate);

            // Assert
            Assert.AreEqual(CONTENT_TYPE_CHARSET, responseToUpdate.Content.Headers.ContentType.ToString());
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldReplaceUserSectorOptions()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToUpdate);
            var userSectorOptions = _context.UserSectorOptons.Where(x => x.UserId == _userId).ToList();

            // Assert
            Assert.AreEqual(3, userSectorOptions.Count);
            CollectionAssert.AreEqual(_userSectorOptionIdsToUpdate, userSectorOptions.Select(x => x.SectorOptionId));
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldRemoveUserSectorOptions()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var userSectorOptionIdsToUpdate = _userSectorOptionIdsToCreate.Skip(2);
            var userDtoToUpdate = new UserDto
            {
                Id = _userId,
                Name = "Fake Name",
                SectorOptionIds = userSectorOptionIdsToUpdate,
            };
            var userDtoJsonToUpdate = new StringContent(
                JsonConvert.SerializeObject(userDtoToUpdate, Formatting.Indented),
                Encoding.UTF8,
                CONTENT_TYPE);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", userDtoJsonToUpdate);
            var userSectorOptions = _context.UserSectorOptons.Where(x => x.UserId == _userId).ToList();

            // Assert
            Assert.AreEqual(1, userSectorOptions.Count);
            CollectionAssert.AreEqual(userSectorOptionIdsToUpdate, userSectorOptions.Select(x => x.SectorOptionId));
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldAddUserSectorOptions()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var userSectorOptionIdsToUpdate = _userSectorOptionIdsToCreate
                .Concat(_context.SectorOptions.AsQueryable()
                    .Skip(3)
                    .Take(3)
                    .Select(x => x.Id));
            var userDtoToUpdate = new UserDto
            {
                Id = _userId,
                Name = "Fake Name",
                SectorOptionIds = userSectorOptionIdsToUpdate,
            };
            var userDtoJsonToUpdate = new StringContent(
                JsonConvert.SerializeObject(userDtoToUpdate, Formatting.Indented),
                Encoding.UTF8,
                CONTENT_TYPE);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", userDtoJsonToUpdate);
            var userSectorOptions = _context.UserSectorOptons.Where(x => x.UserId == _userId).ToList();

            // Assert
            Assert.AreEqual(6, userSectorOptions.Count);
            CollectionAssert.AreEqual(userSectorOptionIdsToUpdate, userSectorOptions.Select(x => x.SectorOptionId));
        }

        [Test]
        public async Task Put_ExistingUser_EndpointShouldUpdateUserName()
        {
            // Act
            _ = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", _userDtoJsonToCreate);
            var userDtoToUpdate = new UserDto
            {
                Id = _userId,
                Name = "Some Other Fake Name",
                SectorOptionIds = _userSectorOptionIdsToCreate,
            };
            var userDtoJsonToUpdate = new StringContent(
                JsonConvert.SerializeObject(userDtoToUpdate, Formatting.Indented),
                Encoding.UTF8,
                CONTENT_TYPE);
            var responseToUpdate = await _client.PutAsync($"{USERS_CONTROLLER_URL}/{_userId}", userDtoJsonToUpdate);
            var actual = _context.Users.SingleOrDefault(x => x.Id == _userId)?.Name;

            // Assert
            Assert.AreEqual(userDtoToUpdate.Name, actual);
        }
    }
}
