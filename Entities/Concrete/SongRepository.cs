using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SongRepository:IEntity
    {
        [Key]
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string SongArtist { get; set; }
        public int SongLikesNumber { get; set; }
        public bool Status { get; set; }


    }
}
