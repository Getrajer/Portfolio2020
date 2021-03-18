using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPGalleriesViewModel
    {
        public JPGalleriesViewModel()
        {
            Albums = new List<JpAlbumDTO>();
            User = new JPUserDTO();
        }

        public List<JpAlbumDTO> Albums { get; set; }
        public JPUserDTO User { get; set; }
    }
}
