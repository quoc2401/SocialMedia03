using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace SocialMedia03.Common.Utils
{
    public static class CloudinaryUtils
    {
        private static readonly Account Account = new Account(
            configs.CLOUDINARY_NAME,
            configs.CLOUDINARY_APP_KEY,
            configs.CLOUDINARY_APP_SECRET);

        public static Cloudinary Cloudinary { get; }
        
        static CloudinaryUtils()
        {
            Cloudinary = new Cloudinary(Account);
            Cloudinary.Api.Secure = true;
        }

        public static JToken UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, file.OpenReadStream())
            };
            var uploadResult = Cloudinary.Upload(uploadParams);

            return uploadResult.JsonObj;
        }
    }
}
