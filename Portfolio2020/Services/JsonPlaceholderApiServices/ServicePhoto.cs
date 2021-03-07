using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServicePhoto : IPhotoService
    {
        private readonly HttpClient httpClient;

        public ServicePhoto(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPPhoto> AddNewPhoto(JPPhoto newPhoto)
        {
            return await httpClient.PostJsonAsync<JPPhoto>($"photos", newPhoto);
        }

        public async Task DeletePhoto(int photoId)
        {
            await httpClient.DeleteAsync($"users/{photoId}");
        }

        public async Task<IEnumerable<JPPhoto>> GetAllPhotos()
        {
            return await httpClient.GetJsonAsync<JPPhoto[]>($"photos");
        }

        public async Task<IEnumerable<JPPhoto>> GetPhotosOfAlbum(int albumId)
        {
            return await httpClient.GetJsonAsync<JPPhoto[]>($"photos/{albumId}/albums");
        }

        public async Task<JPPhoto> UpdatePhoto(JPPhoto photoToUpdate)
        {
            return await httpClient.PutJsonAsync<JPPhoto>($"photos/{photoToUpdate}", photoToUpdate);
        }
    }
}
