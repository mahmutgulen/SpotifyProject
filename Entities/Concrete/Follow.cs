using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Follow : IEntity
    {
        public int id { get; set; }
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
        public bool Status { get; set; }
    }
}
