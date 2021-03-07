using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface IAlbumService
    {
        Task<IEnumerable<JPAlbum>> GetAlbums();
        Task<IEnumerable<JPAlbum>> GetAlbumByUserId(int userId);
        Task<JPAlbum> AddNewAlbum(JPAlbum newAlbum);
        Task<JPAlbum> UpdateAlbum(JPAlbum albumToUpdate);
        Task DeleteAlbum(int albumId);
    }
}
