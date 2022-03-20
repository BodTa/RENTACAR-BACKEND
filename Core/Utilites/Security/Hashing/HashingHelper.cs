using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Security.Hashing
{
    public class HashingHelper
    {
        // Burada iki adet Metodumuz var. CreatePasswordHash olan PasswordHash oluşturuyor 010110011 gibi.
        // passwordSalt ta bizim verdiğimiz SecurityKey olucak.

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        // Verifyda gelen passwordu aynı mantık hashleyip saltlıcaz ve karşılaştırıcaz.
         public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
          {
              using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
              {
                  var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                  for (int i = 0; i < passwordHash.Length; i++)
                  {
                      if (computedHash[i] != passwordHash[i])
                      {
                            return false;
                      }
                  }
              }
              return true;
          }

        public static void CreatePasswordHash(object password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            throw new NotImplementedException();
        }
    }
}
