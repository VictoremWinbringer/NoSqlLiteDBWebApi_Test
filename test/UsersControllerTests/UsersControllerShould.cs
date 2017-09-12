using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using UserApi;
using Xunit;
using Newtonsoft.Json;
using UserApi.Models;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore;

namespace UsersControllerTests
{
    //Провел только позитивные тесты. По идее надо бы еще и негативные выполнить.
   // [TestCaseOrderer("UsersControllerTests.TestCollectionOrderer", "UsersControllerTests")]
    public class UsersControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly User _user;
        public UsersControllerShould()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
           .UseStartup<Startup>());

            _client = _server.CreateClient();

            _user = new User
            {
                FirstName = "Тест",
                LastName = "Тестов",
                Patronymic = "Тестович",
                Phone = "+7(999)7778888"
            };

        }

        public async Task Create()
        {
            var responce = await _client.PostAsync("/api/users/", new StringContent(JsonConvert.SerializeObject(_user)
                , Encoding.UTF8, "application/json"));

            Assert.True(responce.IsSuccessStatusCode);

            var user = JsonConvert.DeserializeObject<User>(await responce.Content.ReadAsStringAsync());

            _user.Id = user.Id;

            Assert.Equal(_user.LastName, user.LastName);
            Assert.Equal(_user.FirstName, user.FirstName);
            Assert.Equal(_user.Patronymic, user.Patronymic);
            Assert.Equal(_user.Phone, user.Phone);
        }

        public async Task ReadAll()
        {
            var responce = await _client.GetAsync("/api/users/");

            Assert.True(responce.IsSuccessStatusCode);

            var users = JsonConvert.DeserializeObject<User[]>(await responce.Content.ReadAsStringAsync());

            Assert.True(users.Length > 0);
            Assert.True(users.Any(u => u.Id == _user.Id));

        }


        public async Task Read()
        {
            var responce = await _client.GetAsync($"/api/users/{_user.Id}");

            Assert.True(responce.IsSuccessStatusCode);

            var user = JsonConvert.DeserializeObject<User>(await responce.Content.ReadAsStringAsync());

            Assert.Equal(_user.LastName, user.LastName);
            Assert.Equal(_user.FirstName, user.FirstName);
            Assert.Equal(_user.Patronymic, user.Patronymic);
            Assert.Equal(_user.Phone, user.Phone);
            Assert.Equal(_user.Id, user.Id);
        }

        public async Task Update()
        {
            _user.FirstName = "Виктор";

            var responce = await _client.PutAsync("/api/users/", new StringContent(JsonConvert.SerializeObject(_user)
                , Encoding.UTF8, "application/json"));

            Assert.True(responce.IsSuccessStatusCode);

            responce = await _client.GetAsync($"/api/users/{_user.Id}");

            Assert.True(responce.IsSuccessStatusCode);

            var user = JsonConvert.DeserializeObject<User>(await responce.Content.ReadAsStringAsync());

            Assert.Equal(_user.LastName, user.LastName);
            Assert.Equal(_user.FirstName, user.FirstName);
            Assert.Equal(_user.Patronymic, user.Patronymic);
            Assert.Equal(_user.Phone, user.Phone);
            Assert.Equal(_user.Id, user.Id);

        }


        public async Task Delete()
        {
            _user.FirstName = "Виктор";

            var responce = await _client.DeleteAsync($"/api/users/{_user.Id}");

            Assert.True(responce.IsSuccessStatusCode);

            responce = await _client.GetAsync($"/api/users/{_user.Id}");

            Assert.True(responce.StatusCode == System.Net.HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task Crud()
        {
            await Create();
            await ReadAll();
            await Read();
            await Update();
            await Delete();
        }
    }
}
