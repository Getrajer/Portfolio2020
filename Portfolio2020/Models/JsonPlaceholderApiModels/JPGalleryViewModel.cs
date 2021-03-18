using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPGalleryViewModel
    {
        public JPGalleryViewModel()
        {
            Photos = new List<JPPhotoDTO>();
        }

        public List<JPPhotoDTO> Photos { get; set; }
        public JPAlbum Album { get; set; }
    }
}
