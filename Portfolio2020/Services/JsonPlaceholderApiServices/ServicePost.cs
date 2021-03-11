using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServicePost : IPostService
    {
        private readonly HttpClient httpClient;

        public ServicePost(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPPost> AddNewPost(JPPost newPost)
        {
            return await httpClient.PostJsonAsync<JPPost>($"posts", newPost);
        }

        public async Task DeletePost(int postId)
        {
            await httpClient.DeleteAsync($"posts/{postId}");
        }

        public async Task<IEnumerable<JPPost>> GetAllPosts()
        {
            return await httpClient.GetJsonAsync<JPPost[]>($"posts");
        }

        public async Task<IEnumerable<JPPost>> GetAllPostsOfUser(int userId)
        {
            return await httpClient.GetJsonAsync<JPPost[]>($"posts/{userId}/users");
        }

        public async Task<JPPost> GetPost(int postId)
        {
            return await httpClient.GetJsonAsync<JPPost>($"posts/{postId}");
        }

        public async Task<JPPost> UpdatePost(JPPost postToUpdate)
        {
            return await httpClient.PutJsonAsync<JPPost>($"posts/{postToUpdate.Id}", postToUpdate);
        }
    }
}
