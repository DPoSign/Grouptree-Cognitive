using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fw8.CognitiveServices.Tags
//namespace IdentityGuesser.Models
{
    public class PoemModel
    {
        //[Key]
        public int PoemModelNumber { get; set; }
        public string ImagePath { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Caption { get; set; }
        public string Tags { get; set; }
        public string Link { get; set; }
        public object File { get; set; }
    }
}
