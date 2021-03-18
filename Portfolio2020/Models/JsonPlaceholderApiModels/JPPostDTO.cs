using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPPostDTO
    {
        public JPPostDTO()
        {
            Comments = new List<JpCommentDTO>();
        }

        public int Id { get; set; }
        public JPPost Post { get; set; }
        public string CommentAddName { get; set; }
        public string CommentAddBody { get; set; }
        public string ErrorResponse { get; set; }
        public bool IfAddComment { get; set; }
        public List<JpCommentDTO> Comments { get; set; }
    }
}
