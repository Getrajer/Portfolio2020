using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPUserDetailsViewModel
    {
        public JPUserDetailsViewModel()
        {
            User = new JPUserDisplay();
        }

        public JPUserDisplay User { get; set; }
    }
}
