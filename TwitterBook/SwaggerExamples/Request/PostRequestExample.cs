using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBook.Contracts.V1.Request;

namespace TwitterBook.SwaggerExamples.Request
{
    public class PostRequestExample : IExamplesProvider<PostRequest>
    {
        public PostRequest GetExamples()
        {
            return new PostRequest
            {
                Name = "Sample Name"
            };
        }
    }
}
