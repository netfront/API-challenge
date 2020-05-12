using Amazing.Persistence;
using Amazing.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Amazing.Application.Repositories
{
    public interface IContentRepository : IBaseRepository<Content, int>
    {
        IQueryable<Content> GetByPost(int postId);
    }

    public class ContentRepository : BaseRepository<Content, int>, IContentRepository
    {
        public ContentRepository(AmazingContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Content> GetByPost(int postId)
            => this.dbContext.Contents.Where(c => c.PostId == postId);
    }
}