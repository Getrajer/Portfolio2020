using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServiceUser : IUserService
    {
        private readonly HttpClient httpClient;

        public ServiceUser(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPUser> AddNewUser(JPUser newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<JPUser> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JPUser>> GetAllUsers()
        {
            return await httpClient.GetJsonAsync<JPUser[]>($"users");
        }

        public async Task<JPUser> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<JPUser> UpdateUser(JPUser userToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
