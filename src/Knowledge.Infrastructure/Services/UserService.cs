using AutoMapper;
using Knowledge.Data.UOW;
using Knowledge.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Knowledge.Infrastructure.Services
{
    public interface IUserService
    {
        Task<string> GetUsers();
    }
    public class UserService : BaseCoreService, IUserService
    {
        private readonly HttpClient _client;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient) : base(unitOfWork, mapper)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client = httpClient;
        }
        public async Task<string> GetUsers()
        {
            var response = await _client.GetAsync("https://localhost:4000/api/users");
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            var responseString = await response.Content.ReadAsStringAsync();
            //var result = JsonSerializer.Deserialize<UserResponse>(responseString);
            return responseString;
        }
    }
}
