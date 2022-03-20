using Core.Utilites.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Helpers
{
    public class FileHelper
    {

        private static string _currentDirectory = Environment.CurrentDirectory + "\\wwwroot"; //Gelen dosyanın kaydediliceği yer'i veriyoruz.
        private static string _folderName = "\\uploads\\images\\"; //Dosya adını images belirledik.

        public static IResult Upload(IFormFile file) 
        {
            var fileExists = CheckFileExists(file); // Gelen file var mı diye bakılıyor.
            if (fileExists.Message != null)
            {
                return new ErrorResult(fileExists.Message); // Var olduğu için hata gönderdik.
            }

            var type = Path.GetExtension(file.FileName); // Uzantıyı alıyoruz.
            var typeValid = CheckFileTypeValid(type); // Type'ı alıyoruz.
            var randomName = Guid.NewGuid().ToString(); // Random bir identify oluşturuyor.

            if (typeValid.Message != null) // Mesaj nulldan farklı ise alttaki kodu çalıştırıyoruz.
            {
                return new ErrorResult(typeValid.Message); // Error gönderiyoruz.
            }

            CheckDirectoryExists(_currentDirectory + _folderName); // Böyle bir şey var mı diye kontrol ediyoruz.
            CreateImageFile(_currentDirectory + _folderName + randomName + type, file); //Bir ImageFile oluşturuyoruz.
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/")); // \\'yı / ile değiştirmişiz.



        }

        public static IResult Update(IFormFile file, string imagePath) // Bir file ile Imaepath isteniyor.
        {
            var fileExists = CheckFileExists(file); // File var mı kontrol etmişiz.
            if (fileExists.Message != null) //Eğer yoksa hata veriyoruz.
            {
                return new ErrorResult(fileExists.Message);
            }

            var type = Path.GetExtension(file.FileName); // Uzantı alıyoruz
            var typeValid = CheckFileTypeValid(type); 
            var randomName = Guid.NewGuid().ToString();

            if (typeValid.Message != null)
            {
                return new ErrorResult(typeValid.Message);
            }

            DeleteOldImageFile((_currentDirectory + imagePath).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, file);
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }

        public static IResult Delete(string path)
        {
            DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
            return new SuccessResult();
        }




        private static IResult CheckFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("File doesn't exists.");
        }


        private static IResult CheckFileTypeValid(string type)
        {
            if (type != ".jpeg" && type != ".png" && type != ".jpg")
            {
                return new ErrorResult("Wrong file type.");
            }
            return new SuccessResult();
        }

        private static void CheckDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fs = File.Create(directory))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private static void DeleteOldImageFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
            {
                File.Delete(directory.Replace("/", "\\"));
            }

        }
    }

}
