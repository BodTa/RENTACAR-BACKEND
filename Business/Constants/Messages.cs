using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {
        public static string Added = "Ekleme işlemi başarılı";
        public static string NameInvalid = "İsim geçersiz";
        public static string Listed = "Listeleme işlemi başarılı";
        public static string MaintenceTime = "Sunucu bakımdadır";
        public static string NotAdded = "Ekleme işlemi başarısız";
        public static string NotListed = "Listeleme işlemi başarısız";
        public static string Deleted = "Kaldırma işlemi başarılı";
        public static string Updated = "Güncelleme işlemi başarılı";
        public static string UserRegistered = "Kayıt başarıyla gerçekleştirildi";

        public static string UserElreadyExists = "Böyle bir email'e kayıtlı kullanıcı bulunmaktadır";

        public static string UserNotFound = "Böyle bir kullanıcı bulunamadı";

        public static string PasswordError = "Girilen şifre hatalıdır";

        public static string SuccessfulLogin = " Başarıyla giriş yapıldı";

        public static string AccessTokenCreated = "Token oluşturuldu";

        public static string AuthorizationDenied = "Yeterli yetkiniz bulunmamaktadır";
    }
}
