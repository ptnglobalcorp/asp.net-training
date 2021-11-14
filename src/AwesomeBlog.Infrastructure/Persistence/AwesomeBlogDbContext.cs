using AwesomeBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AwesomeBlog.Infrastructure.Persistence
{
    public sealed class AwesomeBlogDbContext : DbContext
    {
        // dotnet ef migrations add Initial_Create -p .\AwesomeBlog.Infrastructure\ -s .\AwesomeBlog.Api\ -o Persistence\Scripts

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public AwesomeBlogDbContext(DbContextOptions<AwesomeBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
