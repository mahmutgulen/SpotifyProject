using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.PlayList
{
    public class PlayListAddSongDto:IDto
    {
        public int PL_Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
    }
}
