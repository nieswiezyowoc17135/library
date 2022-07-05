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

        //konfigurowanie tabel
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(x => x.Isbn)
                .HasMaxLength(9);

            modelBuilder.Entity<Book>()
                .Property(x => x.Author)
                .HasColumnType("varchar(20)");
        }
    }
}
