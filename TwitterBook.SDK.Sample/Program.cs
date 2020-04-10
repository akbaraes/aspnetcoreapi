using Refit;
using System;
using System.Threading.Tasks;

namespace TwitterBook.SDK.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwNmM0YTcyNC1kYTQ2LTQzNGEtODg5Zi03NTdmNTliOGVlOTYiLCJyb2xlIjoiQWRtaW4sRWRpdG9yIiwiZW1haWwiOiJha2Jhcmppbm5hLm1AZ21haWwuY29tIiwibmJmIjoxNTg2NDIzNzI0LCJleHAiOjE1ODY0MjM3ODQsImlhdCI6MTU4NjQyMzcyNH0.sKFnr1y62GtdrYCr9SRql0_J8qgQP1ht4zVSAKKoebU";

            var postApi = RestService.For<IPostApi>("http://localhost:5000/", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var posts = await postApi.GetAllAsync();
        }
    }
}
