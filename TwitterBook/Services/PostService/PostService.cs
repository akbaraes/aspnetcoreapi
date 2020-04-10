using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBook.Data;
using TwitterBook.Models;

namespace TwitterBook.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext dbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CheckUserOwnPost(Guid postId, string userId)
        {
            var post = await dbContext.Posts.Where(x => x.Id == postId && x.UserId == userId).FirstOrDefaultAsync();

            return post != null;
        }

        public async Task<bool> CreatePost(Post post)
        {
            dbContext.Posts.Add(post);
            var created = await dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var post = await GetPostById(postId);
            if (post == null)
                return false;

            dbContext.Posts.Remove(post);
            var deleted = await dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Post> GetPostById(Guid id)
        {
            return await dbContext.Posts.FindAsync(id);
        }

        public async Task<List<Post>> GetPosts()
        {


            var dt = await dbContext.Posts.ToListAsync();

            return dt;
        }

        public async Task<bool> UpdatePost(Post updatepost)
        {
            var post = await GetPostById(updatepost.Id);

            if (post == null)
                return false;

            post.Name = updatepost.Name;

            dbContext.Posts.Update(post);
            var updated = await dbContext.SaveChangesAsync();
            return updated > 0;

        }
    }
}
