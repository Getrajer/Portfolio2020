﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        IJSRuntime JSRuntime { get; set; }

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

        public List<JPUserDTO> Users = new List<JPUserDTO>();
        public IEnumerable<JPPhoto> Photos { get; set; }
        public List<JPPostDTO> Posts = new List<JPPostDTO>();

        #region Variables
        protected int post_display_modyfier = 1;
        protected string display_init_loader = "";


        //Photo pop up
        protected bool show_pop_up = false;
        protected JPPhoto PopUpPhoto = new JPPhoto();
        protected int currentPhotoId = 0;


        protected JPUserDetailsViewModel UserDetailsViewModel = new JPUserDetailsViewModel();
        protected JPGalleriesViewModel GetGalleriesViewModel = new JPGalleriesViewModel();
        protected JPGalleryViewModel JPGalleryViewModel = new JPGalleryViewModel();
        #endregion

        #region DisplayPagesVariables
        protected bool display_index_page = true;
        protected bool display_register_page = false;
        protected bool display_main_feed_page = false;
        protected bool display_profile_page = false;
        protected bool display_galleries_page = false;
        protected bool display_gallery_page = false;
        #endregion



        #region DisplayPagesFunctions
        public async Task ResetToggleVariables()
        {
            display_index_page = false;
            display_register_page = false;
            display_main_feed_page = false;
            display_profile_page = false;
            display_galleries_page = false;
            display_gallery_page = false;
        }

        public void DisplayIndex()
        {
            ResetToggleVariables();
            display_index_page = true;
        }

        public void Logoff()
        {
            ResetToggleVariables();
        }

        public void DisplayMyProfile()
        {
            ResetToggleVariables();
            display_profile_page = false;
        }

        public void DisplayRegister()
        {
            ResetToggleVariables();
            display_register_page = true;
        }

        public async Task DisplayMainFeed()
        {
            await ResetToggleVariables();
            display_main_feed_page = true;
            await RenderPosts();
            await RenderUsersForSideBar();
        }

        public async Task DisplayUserProfile(int userId)
        {
            Posts = await GetPostsOfUser(userId);
            UserDetailsViewModel.User.JPUser = await UserService.GetUserById(userId);
            UserDetailsViewModel.User.ProfileImgUrl = $"../images/profile_png_{userId}.png";

            ResetToggleVariables();
            display_profile_page = true;
        }

        public async Task DisplayGalleries(int userId)
        {
            List<JPAlbum> Albums = await GetGalleriesOfUser(userId);
            for(int i = 0; i < Albums.Count; i++)
            {
                JpAlbumDTO jpAlbumDisplay = new JpAlbumDTO();
                jpAlbumDisplay.Album = Albums[i];
                JPPhoto photo = await GetPhotoOfId(1);
                jpAlbumDisplay.ExamplePhotoUrl = photo.ThumbNailUrl;
                GetGalleriesViewModel.Albums.Add(jpAlbumDisplay);
            }

            GetGalleriesViewModel.User = UserDetailsViewModel.User;
            ResetToggleVariables();
            display_galleries_page = true;
        }  

        public async Task DisplayGallery(int albumId)
        {

            JPGalleryViewModel.Photos = await GetPhotossOfAlbumId(albumId);
            JPGalleryViewModel.Album = await GetAlbumOfId(albumId);
            ResetToggleVariables();
            display_gallery_page = true;
        }

        public void ToggleImgPopUp(int photoId)
        {
            if(photoId == -1)
            {
                PopUpPhoto = new JPPhoto();
                show_pop_up = false;
            }
            else
            {
                PopUpPhoto = JPGalleryViewModel.Photos[photoId].Photo;
                currentPhotoId = photoId;
                show_pop_up = true;
            }
        }
        #endregion

        #region Functions for post
        public async Task RenderPosts()
        {
            for(int i = post_display_modyfier; i < post_display_modyfier + 10; i++)
            {
                JPPost post = await PostService.GetPost(post_display_modyfier);

                if(post != null)
                {
                    JPPostDTO pd = new JPPostDTO();
                    IEnumerable<JPComment> comments = await CommentService.GetCommentsOfPosId(post.Id);

                    if(comments != null)
                    {
                        foreach(var c in comments)
                        {
                            JpCommentDTO displayC = new JpCommentDTO();
                            displayC.JPComment = c;
                            int num = RandomNumber(1, 10);
                            displayC.ProfileImgUrl = $"../images/profile_png_{num}.png";
                            pd.Comments.Add(displayC);
                        }
                    }

                    pd.Id = Posts.Count();
                    pd.Post = post;
                    Posts.Add(pd);
                }

            }
        }

        public async Task<List<JPPostDTO>> GetPostsOfUser(int userId)
        {
            List<JPPostDTO> DisplayPosts = new List<JPPostDTO>();
            IEnumerable<JPPost> userPosts = await PostService.GetAllPostsOfUser(userId);

            if(userPosts != null)
            {
                foreach (var post in userPosts)
                {
                    JPPostDTO pd = new JPPostDTO();
                    IEnumerable<JPComment> comments = await CommentService.GetCommentsOfPosId(post.Id);

                    if (comments != null)
                    {
                        foreach (var c in comments)
                        {
                            JpCommentDTO displayC = new JpCommentDTO();
                            displayC.JPComment = c;
                            int num = RandomNumber(1, 10);
                            displayC.ProfileImgUrl = $"../images/profile_png_{num}.png";
                            pd.Comments.Add(displayC);
                        }
                    }

                    JPPost jPPost = post;

                    pd.Id = DisplayPosts.Count();
                    pd.Post = jPPost;
                    DisplayPosts.Add(pd);
                }

                return DisplayPosts;

            }

            return DisplayPosts;

        }
        #endregion

        #region Users Functionality

        public async Task RenderUsersForSideBar()
        {
            IEnumerable<JPUser> users = await UserService.GetAllUsers();

            if(users != null)
            {
                int i = 1;
                foreach(var user in users)
                {
                    JPUserDTO userD = new JPUserDTO();
                    userD.JPUser = user;
                    userD.ProfileImgUrl = $"../images/profile_png_{i}.png";
                    Users.Add(userD);
                    i++;
                    if(i == 10)
                    {
                        i = 1;
                    }
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
                    JpCommentDTO displayC = new JpCommentDTO();
                    displayC.JPComment = responseComment;
                    displayC.ProfileImgUrl = $"../images/profile_png_1.png";
                    Posts[postId].Comments.Add(displayC);
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

        #region Album functions

        public async Task<List<JPAlbum>> GetGalleriesOfUser(int userId)
        {
            IEnumerable<JPAlbum> IAlbums = await AlbumService.GetAlbumByUserId(userId);
            List<JPAlbum> ReturnAlbum = new List<JPAlbum>();
            foreach(var albumV in IAlbums)
            {
                JPAlbum album = albumV;
                ReturnAlbum.Add(album);
            }

            return ReturnAlbum;
        }

        public async Task<JPAlbum> GetAlbumOfId(int albumId)
        {
            return await AlbumService.GetAlbum(albumId);
        }

        #endregion

        #region Photos functions

        public async Task<JPPhoto> GetPhotoOfId(int photoId)
        {
            return await PhotoService.GetPhotoOfId(photoId);
        }

        public async Task<List<JPPhotoDTO>> GetPhotossOfAlbumId(int albumId)
        {
            IEnumerable<JPPhoto> IPhotos = await PhotoService.GetPhotosOfAlbum(albumId);
            List<JPPhotoDTO> ReturnPhotos = new List<JPPhotoDTO>();
            
            foreach(var photoV in IPhotos)
            {
                JPPhotoDTO photoDisplay = new JPPhotoDTO();
                photoDisplay.Photo = photoV;
                photoDisplay.Id = ReturnPhotos.Count();
                ReturnPhotos.Add(photoDisplay);
            }

            return ReturnPhotos;
        }

        #endregion

        protected override async Task OnInitializedAsync()
        {
        }


        #region Helping Functions
        private readonly Random _random = new Random();

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private async Task ScrollTop()
        {
            await JSRuntime.InvokeAsync<string>("ScrollTopJS");

        }
        #endregion
    }
}
