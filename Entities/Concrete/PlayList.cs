using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class PlayList : IEntity
    {
        [Key]
        public int PL_Id { get; set; }
        public int UserId { get; set; }
        public string PL_Name { get; set; }
        public int PL_SongCount { get; set; }
        public int PL_FollowCount { get; set; }
        public DateTime PL_DateTime { get; set; }
        public bool Status { get; set; }
    }
}
