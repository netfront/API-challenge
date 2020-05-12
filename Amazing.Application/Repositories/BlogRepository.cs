using Amazing.Persistence;
using Amazing.Persistence.Models;

namespace Amazing.Application.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog, int>
    {
    }

    public class BlogRepository : BaseRepository<Blog, int>, IBlogRepository
    {
        public BlogRepository(AmazingContext dbContext) : base(dbContext)
        {
        }
    }
}