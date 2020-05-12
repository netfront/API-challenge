using Amazing.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Amazing.Persistence
{
    public class AmazingContext : DbContext
    {
        public AmazingContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(c => c.Blogs).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Blog>().HasMany(c => c.Posts).WithOne(c => c.Blog).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>().HasMany(c => c.Contents).WithOne(c => c.Post).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public AmazingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}