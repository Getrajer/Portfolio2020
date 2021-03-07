using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface ICommentService
    {
        Task<IEnumerable<JPComment>> GetAllComments();
        Task<IEnumerable<JPComment>> GetCommentsOfPosId(int postId);
        Task<JPComment> AddNewComment(JPComment newComment);
        Task<JPComment> UpdateComment(JPComment commentToUpdate);
        Task DeleteComment(int commentId);
    }
}
