using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TwitterBook.Constants.V1;
using TwitterBook.Models;
using Xunit;

namespace TwitterBook.IntegrationTest
{
    public class PostController_Test : IntegrationTest
    {

        [Fact]
        public async Task GetAll_ShouldRetuenEmptyValue()
        {
            await SetAuthenticateKey();

            var response = await TestClient.GetAsync(APIRoute.Post.GetAll);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsAsync<List<Post>>();
            data.Should().HaveCount(0);
        }
    }
}
