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

        public async Task<bool> AddSomeBooks(BookDto book)
        {
            var _book = new Book()
            {
                Author = book.Author,
                Isbn = book.Isbn
            };

            _context.Books.Add(_book);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book == null )
            {
                return false;
            }

            _context.Books.Remove(book);

            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EditBook(int id, BookDto book)
        {
            var _book = _context.Books.FirstOrDefault (x => x.Id == id);

            if (book == null)
            {
                return false;
            }

            _book.Author = book.Author;
            _book.Isbn = book.Isbn;

            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<BookDto> GetOneBook(int id)
        {
            return await _context.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Author = x.Author,
                Isbn = x.Isbn
            }).FirstAsync(x => x.Id == id);
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
