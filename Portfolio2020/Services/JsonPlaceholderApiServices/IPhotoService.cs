using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface IPhotoService
    {
        Task<IEnumerable<JPPhoto>> GetAllPhotos();
        Task<IEnumerable<JPPhoto>> GetPhotosOfAlbum(int albumId);
        Task<JPPhoto> GetPhotoOfId(int photoId);
        Task<JPPhoto> AddNewPhoto(JPPhoto newPhoto);
        Task<JPPhoto> UpdatePhoto(JPPhoto photoToUpdate);
        Task DeletePhoto(int photoId);
    }
}
