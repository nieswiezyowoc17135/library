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
}
);

app.Run();
