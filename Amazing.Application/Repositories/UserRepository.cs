using Amazing.Persistence;
using Amazing.Persistence.Models;
using System.Linq;

namespace Amazing.Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        User Get(string email);

        User Get(string email, string password);
    }

    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(AmazingContext dbContext) : base(dbContext)
        { }

        public User Get(string email, string password) => this.dbContext.Users.FirstOrDefault(c => c.Email == email && c.Password == password);

        public User Get(string email) => this.dbContext.Users.FirstOrDefault(c => c.Email == email);
    }
}