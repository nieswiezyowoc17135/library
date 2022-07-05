using Microsoft.EntityFrameworkCore;

namespace Library.Entities
{
    public class MyBooksContext : DbContext
    {
        public MyBooksContext(DbContextOptions<MyBooksContext> options) : base(options)
        {

        }

        //tworzenie tabeli dla ksiazek
        public DbSet<Book> Books { get; set; }
    }
}
