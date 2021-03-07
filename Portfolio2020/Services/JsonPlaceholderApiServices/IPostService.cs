using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface IPostService
    {
        Task<IEnumerable<JPPost>> GetAllPosts();
        Task<IEnumerable<JPPost>> GetAllPostsOfUser(int userId);
        Task<JPPost> AddNewPost(JPPost newPost);
        Task<JPPost> UpdatePost(JPPost postToUpdate);
        Task DeletePost(int postId);
    }
}
