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
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IPhotoService PhotoService { get; set; }

        public IEnumerable<JPUser> Users { get; set; }
        public IEnumerable<JPPhoto> Photos { get; set; }

        protected int count = 0;

        public void Up()
        {
            count++;
        }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserService.GetAllUsers();
            Photos = await PhotoService.GetAllPhotos();
        }
    }
}
