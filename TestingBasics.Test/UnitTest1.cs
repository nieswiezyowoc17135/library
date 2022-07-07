using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FluentAssertions;

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
            new Book { Id=1, Author = "JakisAutor", Isbn = "123456789" },
            new Book { Id=2, Author = "JakisDrugiAutor", Isbn = "123456789" }
            );

        _context.SaveChanges();
    }

    [Fact]
    public void GettingAllBooks()
    {
        //Arrange
        UnitTest1 dbContext = new UnitTest1();

        //Act

        
        //Assert
    }
}