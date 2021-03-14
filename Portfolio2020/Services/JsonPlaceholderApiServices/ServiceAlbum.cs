using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServiceAlbum : IAlbumService
    {
        private readonly HttpClient httpClient;

        public ServiceAlbum(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPAlbum> AddNewAlbum(JPAlbum newAlbum)
        {
            return await httpClient.PostJsonAsync<JPAlbum>($"albums", newAlbum);
        }

        public async Task DeleteAlbum(int albumId)
        {
            await httpClient.DeleteAsync($"albums/{albumId}");
        }

        public async Task<JPAlbum> GetAlbum(int albumId)
        {
            return await httpClient.GetJsonAsync<JPAlbum>($"albums/{albumId}");
        }

        public async Task<IEnumerable<JPAlbum>> GetAlbumByUserId(int userId)
        {
            return await httpClient.GetJsonAsync<JPAlbum[]>($"albums/{userId}/users");
        }

        public async Task<IEnumerable<JPAlbum>> GetAlbums()
        {
            return await httpClient.GetJsonAsync<JPAlbum[]>($"albums");
        }

        public async Task<JPAlbum> UpdateAlbum(JPAlbum albumToUpdate)
        {
            return await httpClient.PutJsonAsync<JPAlbum>($"albums", albumToUpdate);
        }
    }
}
