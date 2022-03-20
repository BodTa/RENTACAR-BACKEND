using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Security.Encrypiton
{
    // Signing => imzalama // Credentials => kimlik bilgileri ===> Kimlik bilgileri imzalama veya doğrulama
    // SecurityKeyleri alıp bir kimlik doğrulama yapıyoruz. Burası ne için olucak şifreleme işlemlerimiz için lazım olucak.
   
   public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
