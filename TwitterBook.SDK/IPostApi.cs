using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBook.Constants.V1;
using TwitterBook.Contracts.V1.Response;

namespace TwitterBook.SDK
{
    [Headers("Authorization: Bearer")]
    public interface IPostApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<List<PostResponse>>> GetAllAsync();


    }
}
