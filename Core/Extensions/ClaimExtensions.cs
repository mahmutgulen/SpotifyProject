
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        //
        public static void AddNickName(this ICollection<Claim> claims, string givenanme)
        {
            claims.Add(new Claim(ClaimTypes.GivenName, givenanme));
        }

        public static void AddSurname(this ICollection<Claim> claims, string surname)
        {
            claims.Add(new Claim(ClaimTypes.Surname, surname));
        }



        public static void AddNameIdentyfier(this ICollection<Claim> claims, string nameIdentyfier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentyfier));
        }


        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }


    }
}
