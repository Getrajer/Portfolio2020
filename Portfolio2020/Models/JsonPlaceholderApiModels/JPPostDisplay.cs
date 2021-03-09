using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Models.JsonPlaceholderApiModels
{
    public class JPPostDisplay
    {
        public JPPost Post { get; set; }
        public IEnumerable<JPComment> Comments { get; set; }
    }
}
