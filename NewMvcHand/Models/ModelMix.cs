using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMvcHand.Models
{
    public class ModelMix
    {
        public IEnumerable<Gallery> Galleries { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}