using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class GalleriesViewModel
    {
        public GalleriesViewModel()
        {
            Albums = new List<JpAlbumDisplay>();
            User = new JPUserDisplay();
        }

        public List<JpAlbumDisplay> Albums { get; set; }
        public JPUserDisplay User { get; set; }
    }
}
