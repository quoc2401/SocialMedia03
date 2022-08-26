using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common
{
    public class configs
    {
        //size config
        public static readonly int POST_PAGE_SIZE = 20;
        public static readonly int COMMENT_PAGE_SIZE = 10;
        public static readonly int NOTIF_PAGE_SIZE = 10;

        //cloudinary config
        public static readonly string CLOUDINARY_NAME = "quoc2401";
        public static readonly string CLOUDINARY_APP_KEY = "847526293459521";
        public static readonly string CLOUDINARY_APP_SECRET = "Ahw_gyTqo8_Nq_4yupKm0BJ5uto";

        //mail config
        public static readonly int MAIL_PORT = 587;
        public static readonly string MAIL_HOST = "smtp.gmail.com";
        public static readonly string MAIL_USERNAME = "honguyencongsang.dev@gmail.com";
        public static readonly string MAIL_PASSWORD = "rpjjaxemfxudcjga";

        //fbconfig
        public static readonly string FB_APP_ID="800114437619089";
        public static readonly string FB_APP_KEY = "0a140442c3bb088fa551da048a297bbc";
        public static readonly string FB_REDIRECT_URL = "http://localhost:7155/login-facebook";
        public static readonly string FB_LINK_GET_TOKEN = "https://graph.facebook.com/oauth/access_token?client_id=%s&client_secret=%s&redirect_uri=%s&code=%s";
    }
}
