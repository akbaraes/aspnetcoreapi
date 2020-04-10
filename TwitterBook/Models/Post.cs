using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBook.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


        public string UserId { get; set; }
    }
}
