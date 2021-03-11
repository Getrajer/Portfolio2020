using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPPostDisplay
    {
        public JPPostDisplay()
        {
            Comments = new List<JPComment>();
        }

        public int Id { get; set; }
        public JPPost Post { get; set; }
        public string CommentAddName { get; set; }
        public string CommentAddBody { get; set; }
        public string ErrorResponse { get; set; }
        public bool IfAddComment { get; set; }
        public List<JPComment> Comments { get; set; }
    }
}
