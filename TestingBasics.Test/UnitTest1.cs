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
            new Book { Id = 1, Author = "JakisAutor", Isbn = "987654321" },
            new Book { Id = 2, Author = "JakisDrugiAutor", Isbn = "123456789" }
            );

        _context.SaveChanges();
    }

    //scenariusze 

    [Fact]
    public async void GettingAllBooks()
    {
        //Arrange (tworzenie nowego serwisu)
        BookService service = new BookService(_context);

        //Act (tutaj wyprowadzam dane z serwisu)
        List<BookDto> listOfBooks = await service.GetAllBooks();
        //tworznie nowej listy obiektów BookDto, któr¹ potem bêdê porównywa³
        var expected = await _context.Books.Select(x => new BookDto
        {
            Id = x.Id,
            Author = x.Author,
            Isbn = x.Isbn
        }).ToListAsync();

        //Assert (tutaj porownuje z danymi, które mam aktualnie w bazie danych "in memory")
        expected.Should().BeEquivalentTo(listOfBooks);
    }

    [Fact]
    public async void GettingOneBook()
    {
        //Arrange (tworzenie nowego serwisu)
        BookService service = new BookService(_context);

        //Act (tutaj wprowadzam dane z serwisu)
        int id = 1;
        var foundBook = await service.GetOneBook(id);
        var expected = await _context.Books.Select(x => new BookDto
        {
            Id = x.Id,
            Author = x.Author,
            Isbn = x.Isbn
        }).FirstAsync(x => x.Id == id);

        //Assert (porównanie danych)
        expected.Should().BeEquivalentTo(foundBook);
    }

    [Fact]
    public async void AddingBook()
    {
        //Arrange (tworzenie nowego serwisu)
        BookService service = new BookService(_context);

        //Act
        //deklaracja nowej ksiazki
        BookDto newBook = new BookDto()
        {
            Id = 3,
            Author = "JakisTrzeciAutor",
            Isbn = "135798642"
        };

        //dodanie nowej ksiazki do bazy
        await service.AddSomeBooks(newBook);

        //pobranie ksiazki z bazy 
        var expected = await _context.Books.Select(x => new BookDto
        {
            Id = x.Id,
            Author = x.Author,
            Isbn = x.Isbn
        }).FirstAsync(x => x.Id == 3);

        //Porównanie elementów
        expected.Should().BeEquivalentTo(newBook);
    }

    [Fact]
    public async void DeletingBook()
    {
        //Arrange
        BookService service = new BookService(_context);

        //Act
        await service.DeleteBook(1);
        var expected = await _context.Books.FirstOrDefaultAsync(x => x.Id == 1);

        expected.Should().BeNull();
    }

    [Fact]
    public async void EditingBook()
    {
        //Arrange
        BookService service = new BookService(_context);

        //Act
        int id = 1;
        BookDto expected = new BookDto()
        {
            Id = 4,
            Author = "JakisCzwartyAutor",
            Isbn = "123654789"
        };

        await service.EditBook(id, expected);

        var result = await _context.Books.Select(x => new BookDto
        {
            Id = x.Id,
            Author = x.Author,
            Isbn = x.Isbn
        }).FirstAsync(x => x.Id == 1);


        //Assert
        result.Author.Should().BeEquivalentTo(expected.Author);
        result.Isbn.Should().BeEquivalentTo(expected.Isbn);
     }
}