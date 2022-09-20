using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAll()
        {
            var usersResponse = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            if(usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}

