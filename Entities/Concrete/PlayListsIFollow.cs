using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PlayListsIFollow:IEntity
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int PL_Id { get; set; }
        public bool Status { get; set; }
    }
}
