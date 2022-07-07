using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FluentAssertions;
using Library.Services;
using Library.Services.Models;

namespace TestingBasics.Test;

public class UnitTest1
{
    //nasza baza danych z dwoma obiektami 
    private MyBooksContext _context;
    private DbContextOptions<MyBooksContext> _contextOptions;

    //konstruktor z inicjalizowaniem bazy
    public UnitTest1()
    {
        _contextOptions = new DbContextOptionsBuilder<MyBooksContext>()
            //nie wiem co to znaczy
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _context = new MyBooksContext(_contextOptions);

        _context.Database.EnsureDeletedAsync();
        _context.Database.EnsureCreatedAsync();

        _context.AddRange(
            new Book { Id=1, Author = "JakisAutor", Isbn = "987654321" },
            new Book { Id=2, Author = "JakisDrugiAutor", Isbn = "123456789" }
            );

        _context.SaveChanges();
    }

    [Fact]
    public async void GettingAllBooks()
    {
        //Arrange (tworzenie nowego serwisu)
        BookService service = new BookService(_context);

        //Act (tutaj wyprowadzam dane z serwisu)
        List<BookDto> listOfBooks = await Task.Run(() => service.GetAllBooks());

        //Assert (tutaj porownuje z danymi, które mam aktualnie w bazie danych "in memory")
        service.Should().BeEquivalentTo(listOfBooks);
    }
}