using System.Reflection;
using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Book> Books  { get; set; }
    public DbSet<Author>Authors { get; set; }
    public DbSet<Category> Categories {get; set;}
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity("BookTags").HasData(
            new { BooksId = 1, TagsId = 1 },
            new { BooksId = 1, TagsId = 5 },
            new { BooksId = 1, TagsId = 7 },

            new { BooksId = 2, TagsId = 3 },

            new { BooksId = 3, TagsId = 1 },
            new { BooksId = 3, TagsId = 6 },

            new { BooksId = 4, TagsId = 1 },
            new { BooksId = 4, TagsId = 5 },

            new { BooksId = 5, TagsId = 2 },

            new { BooksId = 6, TagsId = 4 }
        );
    }
}