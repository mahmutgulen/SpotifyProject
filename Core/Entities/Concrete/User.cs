using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public string UserName{ get; set; }
        public string UserSurname{ get; set; }
        public string UserNickName{ get; set; }
        public string UserEmail{ get; set; }
        public byte[] UserPasswordSalt { get; set; }
        public byte[] UserPasswordHash { get; set; }
        public bool Status { get; set; } = false;
        public int Followers { get; set; }

    }
}
