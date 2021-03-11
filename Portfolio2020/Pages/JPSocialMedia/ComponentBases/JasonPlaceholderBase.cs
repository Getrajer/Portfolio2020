using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using Portfolio2020.Services.JsonPlaceholderApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Pages.JPSocialMedia.ComponentBases
{
    public class JasonPlaceholderBase : ComponentBase
    {
        #region InjectingServices

        [Inject]
        public IAlbumService AlbumService { get; set; }

        [Inject]
        public ICommentService CommentService { get; set; }

        [Inject]
        public IPhotoService PhotoService { get; set; }

        [Inject]
        public IPostService PostService { get; set; }

        [Inject]
        public ITodosService TodosService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        #endregion

        public IEnumerable<JPUser> Users { get; set; }
        public IEnumerable<JPPhoto> Photos { get; set; }
        public List<JPPostDisplay> Posts = new List<JPPostDisplay>();

        #region Variables
        protected int post_display_modyfier = 1;
        protected string display_init_loader = "";
        #endregion

        #region DisplayPagesVariables
        protected bool display_index_page = false;
        protected bool display_register_page = false;
        protected bool display_main_feed_page = true;
        protected bool display_profile_page = false;
        protected bool display_galleries_page = false;
        protected bool display_gallery_page = false;
        #endregion

        #region DisplayPagesFunctions
        public void ResetToggleVariables()
        {
            display_index_page = false;
            display_register_page = false;
            display_main_feed_page = false;
            display_profile_page = false;
            display_galleries_page = false;
            display_gallery_page = false;
        }

        public void DisplayRegister()
        {
            ResetToggleVariables();
            display_register_page = true;
        }

        public async Task DisplayMainFeed()
        {
            await RenderPosts();

            ResetToggleVariables();
            display_main_feed_page = true;


        }
        #endregion

        #region Render functions for post
        public async Task RenderPosts()
        {
            for(int i = post_display_modyfier; i < post_display_modyfier + 10; i++)
            {
                JPPost post = await PostService.GetPost(post_display_modyfier);

                if(post != null)
                {
                    JPPostDisplay pd = new JPPostDisplay();
                    IEnumerable<JPComment> comments = await CommentService.GetCommentsOfPosId(post.Id);

                    if(comments != null)
                    {
                        foreach(var c in comments)
                        {
                            pd.Comments.Add(c);
                        }
                    }

                    pd.Id = Posts.Count();
                    pd.Post = post;
                    Posts.Add(pd);
                }

            }
        }
        #endregion


        #region Comments functionality
        public void ToggleAddComment(int postId)
        {
            if (Posts[postId].IfAddComment)
            {
                Posts[postId].IfAddComment = false;
                Posts[postId].CommentAddBody = "";
                Posts[postId].CommentAddName = "";
            }
            else
            {
                Posts[postId].IfAddComment = true;
            }
        }

        public async Task AddComment(int postId)
        {
            if(Posts[postId].CommentAddBody != "")
            {
                JPComment addedComment = new JPComment();

                addedComment.Body = Posts[postId].CommentAddBody;
                addedComment.Name = Posts[postId].CommentAddName;
                addedComment.Email = "test@test.com";
                addedComment.PostId = Posts[postId].Post.Id;

                JPComment responseComment = await CommentService.AddNewComment(addedComment);


                if (responseComment != null)
                {
                    Posts[postId].Comments.Add(responseComment);
                    Posts[postId].CommentAddBody = "";
                    Posts[postId].CommentAddName = "";
                    ToggleAddComment(postId);
                }
                else
                {
                    Posts[postId].ErrorResponse = "Something went wrong with adding the comment. Try again.";
                }
            }
        }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            await RenderPosts();
            display_init_loader = "display_none";

        }
    }
}
