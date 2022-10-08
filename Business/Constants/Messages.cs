using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {
        public static string Added = "Successfuly added";
        public static string NameInvalid = "Invalid name";
        public static string Listed = "Listed Successfuly";
        public static string MaintenceTime = "Maintain time";
        public static string NotAdded = "Adding does not completed.";
        public static string NotListed = "Listing does not completed.";
        public static string Deleted = "Successfuly deleted.";
        public static string Updated = "Successfuly updated.";
        public static string UserRegistered = "Successfuly signed up.";

        public static string UserElreadyExists = "User already exist.";

        public static string UserNotFound = "User does not exist.";

        public static string PasswordError = "Email or Password is wrong.";

        public static string SuccessfulLogin = "Sign in successfuly.";

        public static string AccessTokenCreated = "Token created.";

        public static string AuthorizationDenied = "Not enough permission.";
    }
}
