using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Infrastructure.Data.SeedData;

public static class SeedData
{
   
   public static List<Author> CreateAuthors => new()
   {
      new Author
      {
         Id = 1,
         FullName = "Robert C. Martin",
         BirthDate = new DateTime(1952, 12, 5),
         Country = "USA"
      },
      new Author
      {
         Id = 2,
         FullName = "Jon Skeet",
         BirthDate = new DateTime(1976, 6, 19),
         Country = "UK"
      },
      new Author
      {
         Id = 3,
         FullName = "James Clear",
         BirthDate = new DateTime(1986, 1, 22),
         Country = "USA"
      }
   };
   public static List<Category> Categories => new()
   {
      new Category
      {
         Id = 1,
         Title = "Programming",
         Description = "Programming and software engineering books"
      },
      new Category
      {
         Id = 2,
         Title = "Self Development",
         Description = "Personal growth books"
      },
      new Category
      {
         Id = 3,
         Title = "Productivity",
         Description = "Focus and productivity books"
      }
   };

   public static List<Book> Books => new()
   {
      new Book
      {
         Id = 1,
         Title = "Clean Code",
         Price = 700,
         PublishYear = 2008,
         Stock = 4,
         CreatedAt = new DateTime(2024, 1, 1),
         AuthorId = 1,
         CategoryId = 1,
         PublisherId = 1
      },
      new Book
      {
         Id = 2,
         Title = "C# In Depth",
         Price = 850,
         PublishYear = 2019,
         Stock = 4,
         CreatedAt = new DateTime(2024, 1, 2),
         AuthorId = 2,
         CategoryId = 1,
         PublisherId = 2
      },
      new Book
      {
         Id = 3,
         Title = "Atomic Habits",
         Price = 450,
         PublishYear = 2018,
         Stock = 4,
         CreatedAt = new DateTime(2024, 1, 3),
         AuthorId = 3,
         CategoryId = 2,
         PublisherId = 3
      },
      new Book
      {
         Id = 4,
         Title = "The Pragmatic Programmer",
         Price = 900,
         PublishYear = 1999,
         Stock = 3,
         CreatedAt = new DateTime(2024, 1, 4),
         AuthorId = 1,
         CategoryId = 1,
         PublisherId = 2
      },
      new Book
      {
         Id = 5,
         Title = "Deep Work",
         Price = 500,
         PublishYear = 2016,
         Stock = 2,
         CreatedAt = new DateTime(2024, 1, 5),
         AuthorId = 3,
         CategoryId = 3,
         PublisherId = 1
      },
      new Book
      {
      Id = 6,
      Title = "EF Core in Action",
      Price = 600,
      PublishYear = 2021,
      Stock = 5,
      CreatedAt = new DateTime(2024, 1, 6),
      AuthorId = 2,
      CategoryId = 1,
      PublisherId = 2
   }
   };
   
   public static List<Publisher> Publishers => new()
   {
      new Publisher
      {
         Id = 1,
         CreatedAt =new DateTime(2024, 1, 5), 
         Name = "Pearson",
         City = "New York",
         PhoneNumber = "111-222-333"
      },
      new Publisher
      {
         Id = 2,
         CreatedAt =new DateTime(2024, 1, 4),
         Name = "OReilly",
         City = "California",
         PhoneNumber = "444-555-666"
      },
      new Publisher
      {
         Id = 3,
         CreatedAt =new DateTime(2024, 1, 22),
         Name = "HarperCollins",
         City = "London",
         PhoneNumber = "777-888-999"
      }
   };
   public static List<Tag> Tags => new()
   {
      new Tag { Id = 1, Name = "Programming" },
      new Tag { Id = 2, Name = "Database" },
      new Tag { Id = 3, Name = "CSharp" },
      new Tag { Id = 4, Name = "EFCore" },
      new Tag { Id = 5, Name = "DesignPattern" },
      new Tag { Id = 6, Name = "Beginner" },
      new Tag { Id = 7, Name = "Advanced" }
   };
   

   
}