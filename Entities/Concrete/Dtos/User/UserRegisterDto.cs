using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.User
{
    public class UserRegisterDto : IDto
    {
        [EmailAddress]
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserNickName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserSurname { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
