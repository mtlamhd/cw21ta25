// See https://aka.ms/new-console-template for more information

using CW21Ta23.Domain.Dto;
using CW21Ta23.Infrastructure.Data;
using CW21Ta23.Infrastructure.Repositiries;
using CW21Ta23.Service;

// var context = new AppDbContext();
// var bookRepo = new BookRepository(context);
// var categoryRepo = new CategoryRepository(context);
// var publisherRepo = new PublisherRepository(context);
// var authorRepo = new AuthorReposiory(context);
// var tagRepo = new TagRepository(context);
// var bookService = new BookService(bookRepo,authorRepo, categoryRepo, publisherRepo,tagRepo);

// Console.Write("Book Id  ");
// int bookId = int.Parse(Console.ReadLine());
//
// Console.Write("Stock : ");
// int newStock = int.Parse(Console.ReadLine());
//
// var dto = new UpdateBookStockDto
// {
//     BookId = bookId,
//     NewStock = newStock
// };
//
// var result = await bookService.UpdateBookStock(dto);
//
// Console.WriteLine(result);



// Console.Write("Book Id را وارد کنید: ");
// int id = int.Parse(Console.ReadLine());
//
// var book = await bookService.GetBookWithDetailsAsync(id);
//
// if (book == null)
// {
//     Console.WriteLine("خطا: کتاب مورد نظر یافت نشد.");
// }
// else
// {
//     Console.WriteLine($"عنوان: {book.Title}");
//     Console.WriteLine($"قیمت: {book.Price}");
//     Console.WriteLine($"موجودی: {book.Stock}");
//     Console.WriteLine($"سال: {book.PublishYear}");
//     Console.WriteLine($"نویسنده: {book.AuthorName}");
//     Console.WriteLine($"دسته‌بندی: {book.CategoryName}");
//     Console.WriteLine($"ناشر: {book.PublisherName}");
//     Console.WriteLine($"تگ‌ها: {string.Join(", ", book.Tags)}");
// }

Console.Write("sss");