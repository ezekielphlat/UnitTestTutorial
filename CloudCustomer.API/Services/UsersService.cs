using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomer.API.Services
{
 
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiOptions;

        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiOptions)
        {
            this._httpClient = httpClient;
            this._apiOptions = apiOptions.Value;
        
        }

        public IOptions<UsersApiOptions> ApiOptions { get; }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiOptions.Endpoint);
            if(usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
           return  allUsers?.ToList() ?? new List<User>(); 
        }
    }
}
