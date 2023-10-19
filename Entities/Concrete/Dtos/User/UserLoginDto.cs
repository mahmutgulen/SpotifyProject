using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.User
{
    public class UserLoginDto : IDto
    {
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
