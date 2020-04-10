using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBook.Models;

namespace TwitterBook.Services.PostService
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();

        Task<Post> GetPostById(Guid userId);

        Task<bool> CreatePost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(Guid postId);

        Task<bool> CheckUserOwnPost(Guid postId, string v);
    }
}
