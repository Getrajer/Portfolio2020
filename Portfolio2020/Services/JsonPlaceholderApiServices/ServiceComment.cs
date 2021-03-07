using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServiceComment : ICommentService
    {
        private readonly HttpClient httpClient;

        public ServiceComment(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPComment> AddNewComment(JPComment newComment)
        {
            return await httpClient.PostJsonAsync<JPComment>($"comments", newComment);
        }

        public async Task DeleteComment(int commentId)
        {
            await httpClient.DeleteAsync($"comments/{commentId}");
        }

        public async Task<IEnumerable<JPComment>> GetAllComments()
        {
            return await httpClient.GetJsonAsync<JPComment[]>($"comments");
        }

        public async Task<IEnumerable<JPComment>> GetCommentsOfPosId(int postId)
        {
            return await httpClient.GetJsonAsync<JPComment[]>($"comments/{postId}/posts");
        }

        public async Task<JPComment> UpdateComment(JPComment commentToUpdate)
        {
            return await httpClient.PutJsonAsync<JPComment>($"comments/{commentToUpdate.Id}", commentToUpdate);
        }
    }
}
