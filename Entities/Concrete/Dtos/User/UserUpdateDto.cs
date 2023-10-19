using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.User
{
    public class UserUpdateDto:IDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserNickName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool Status { get; set; } = false;
        public int UserRoleId { get; set; }
    }
}
