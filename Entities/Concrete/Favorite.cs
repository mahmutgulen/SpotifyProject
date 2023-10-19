using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{

    public class Favorite : IEntity
    {
        [Key]
        public int FavoritesId { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public bool Status { get; set; }
    }
}
