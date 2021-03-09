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
            IEnumerable<JPPost> postsGet = await PostService.GetAllPosts();

            if (postsGet != null)
            {
                foreach (var p in postsGet)
                {
                    JPPostDisplay pd = new JPPostDisplay();
                    pd.Post = p;
                    IEnumerable<JPComment> jPComments = await CommentService.GetCommentsOfPosId(p.Id);

                    if (jPComments != null)
                    {
                        pd.Comments = jPComments;
                    }

                    Posts.Add(pd);
                }
            }

            ResetToggleVariables();
            display_main_feed_page = true;


        }
        #endregion


        protected override async Task OnInitializedAsync()
        {
            IEnumerable<JPPost> postsGet = await PostService.GetAllPosts();

            if (postsGet != null)
            {
                foreach (var p in postsGet)
                {
                    JPPostDisplay pd = new JPPostDisplay();
                    pd.Post = p;
                    IEnumerable<JPComment> jPComments = await CommentService.GetCommentsOfPosId(p.Id);

                    if (jPComments != null)
                    {
                        pd.Comments = jPComments;
                    }

                    Posts.Add(pd);
                }
            }

        }
    }
}
