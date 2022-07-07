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
                /*Id = book.Id,*/
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
            //można to tak zapisać
            /*return await _context.SaveChangesAsync() == 1;*/
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = _context.Books.First(x => x.Id == id);
            _context.Books.Remove(book);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// /////////////////////////////////////////// nie dziala
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<bool> EditBook(int id, BookDto book)
        {
            var _book = _context.Books.First(x => x.Id == id);
            _book.Author = book.Author;
            _book.Isbn = book.Isbn;
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
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
