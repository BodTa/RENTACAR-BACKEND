using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Security.Encrypiton
{
    public class SecurityKeyHelper
    {
        // Bu program.json da belirlediğimiz SecurityKey'i almamız için olucak.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
