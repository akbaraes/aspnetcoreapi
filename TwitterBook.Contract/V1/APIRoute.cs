using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBook.Constants.V1
{
    public static class APIRoute
    {
        public const string Root = "api";

        public const string Version = "V1";

        public const string Base = Root + "/" + Version;

        public static class Post
        {
            public const string GetAll = Base + "/posts";

            public const string Get = Base + "/posts/{postId}";

            public const string Create = Base + "/posts";

            public const string Update = Base + "/posts/{postId}";

            public const string Delete = Base + "/posts/{postId}";

            public const string MultipleCreate = Base + "/posts/AddMultiple";



        }
    }
}
