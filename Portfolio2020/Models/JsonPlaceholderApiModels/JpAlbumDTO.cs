using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JpAlbumDTO
    {
        public JpAlbumDTO()
        {
            Album = new JPAlbum();
            ExamplePhotoUrl = "";
        }

        public JPAlbum Album { get; set; }
        public string ExamplePhotoUrl { get; set; }
    }
}
