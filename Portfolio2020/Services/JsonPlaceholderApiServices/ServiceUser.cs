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
            return await httpClient.PostJsonAsync<JPUser>($"users", newUser);
        }

        public async Task DeleteUser(int userId)
        {
            await httpClient.DeleteAsync($"users/{userId}");
        }

        public async Task<IEnumerable<JPUser>> GetAllUsers()
        {
            return await httpClient.GetJsonAsync<JPUser[]>($"users");
        }

        public async Task<JPUser> GetUserById(int userId)
        {
            return await httpClient.GetJsonAsync<JPUser>($"users/{userId}");
        }

        public async Task<JPUser> UpdateUser(JPUser userToUpdate)
        {
            return await httpClient.PutJsonAsync<JPUser>($"users", userToUpdate);
        }
    }
}
