﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class UserDetailsViewModel
    {
        public JPUserDisplay User { get; set; }
        public List<JPPostDisplay> Posts { get; set; }
    }
}
