using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using Portfolio2020.Services.JsonPlaceholderApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Pages.JPSocialMedia.ComponentBases
{
    public class JPIndexBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        public IEnumerable<JPUser> Users { get; set; }

        public async Task LoadUsers()
        {
            Users = await UserService.GetAllUsers();
        }
    }
}
