﻿using Library.Entities;
using Library.Services.Interfaces;
using Library.Services.Models;
using System.Data.Entity;

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

        public Task<long> AddSomeBooks(BookDto book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public Task EditBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetBooks()
        {
            //zwracanie listy, która tworzy się asynchronicznie z obiektów, które są w bazie
            return _context.Books.Select(x => new BookDto
            {
                /*Id = x.Id,*/
                Author = x.Author,
                Isbn = x.Isbn,
            }).ToListAsync();
        }
    }
}
