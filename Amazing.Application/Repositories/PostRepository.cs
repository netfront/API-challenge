using Amazing.Persistence;
using Amazing.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Amazing.Application.Repositories
{
    public interface IPostRepository : IBaseRepository<Post, int>
    {
        IQueryable<Post> GetByBlog(int blogId);
    }

    public class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(AmazingContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Post> GetByBlog(int blogId) => this.dbContext.Posts.Where(c => c.BlogId == blogId);
    }
}