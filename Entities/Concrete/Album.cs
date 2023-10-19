using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Album : IEntity
    {
        [Key]
        public int id { get; set; }
        public string AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string AlbumArtist { get; set; }
        public bool Status { get; set; }
    }
}
