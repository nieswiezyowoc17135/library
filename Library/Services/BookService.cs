using Library.Entities;
using Library.Services.Interfaces;
using Library.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        //zmienna, w ktorej beda wszystkie dane z bazy danych, ktore przypisujemy przy wywolywaniu konstruktora
        private readonly MyBooksContext _context;

        public BookService(MyBooksContext context)
        {
            _context = context;
        }

        public async Task AddSomeBooks(BookDto book)
        {
            var _book = new Book()
            {
                Id = book.Id,
                Author = book.Author,
                Isbn = book.Isbn
            };

            _context.Books.Add(_book);
            await _context.SaveChangesAsync();

        }

        public Task DeleteBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public Task EditBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public async Task<BookDto> GetOneBook(int id)
        {
            return await _context.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Author = x.Author,
                Isbn = x.Isbn
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            //zwracanie listy, która tworzy się asynchronicznie z obiektów, które są w bazie
            return await _context.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Author = x.Author,
                Isbn = x.Isbn
            }).ToListAsync();
        }
    }
}
