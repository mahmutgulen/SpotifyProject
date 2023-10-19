using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AlbumApi : IEntity
    {

        public string name { get; set; }
        public List<Artist> artists { get; set; }


        public class Artist
        {
            public string name { get; set; }
        }
    }
}
