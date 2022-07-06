using Library.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//definicja rejestracji
builder.Services.AddDbContext<MyBooksContext>(
    //wskazanie ze dla DbContextu chcemy uzywac sqlServera o danym connection stringu
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBooksConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//mapowanie zeby pobrac dane (endpoint to data)
app.MapGet("data", (MyBooksContext db) =>
{
    var books = db.Books.ToList();
    return books;
});

//mapowanie zeby dodac dane do bazydanych 
app.MapPost("update", (MyBooksContext db) =>
{
    Book book = new Book()
    {
        Author = "Pawel",
        Isbn = "123456789"
    };

    Book book1 = new Book()
    {
        Author = "Gawel",
        Isbn = "987654321"
    };

    var books = new List<Book>() { book, book1 };
    db.Books.AddRange(books);
    db.SaveChanges();
});

//mapowanie zeby zrobic update danych na id 3
app.MapPut("edit", (MyBooksContext db) =>
{
    Book book = db.Books.First(book => book.Id == 3);
    book.Author = "Stefan";
    db.SaveChanges();
});

//mapowanie zeby usunac dane o danych id
app.MapDelete("delete", (MyBooksContext db) =>
{
    Book book = db.Books.First(book => book.Id == 1);
    db.Remove(book);
    db.SaveChanges();
});

app.Run();
