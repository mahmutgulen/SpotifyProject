using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Library : IEntity
    {
        [Key]
        public int id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int PL_Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        //public List<FavoriteListDto> Favorite { get; set; }
    }

}

