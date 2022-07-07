using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Entities;
using Library.Services.Interfaces;
using Library.Services;
using Library.Services.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        //w kontrolerze robie sobie serwis zamiast contextu, potrzebne w razie kilku kontrolerów
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// ////////////////////////////////////////////////////////////////////////////////      Zrobione
        /// <returns></returns>
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks()
        {
            if (_bookService == null)
            {
                return NotFound();
            } else
            {
                return await _bookService.GetAllBooks();
            }
        }

        /// /////////////////////////////////////////////////////////////////////////////////      Zrobione
        /// <returns></returns>
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            /*var book = await _bookService.GetOneBook(id);*/
            if (_bookService == null)
            {
                return NotFound();
            } else
            {
                return await _bookService.GetOneBook(id);
            }

        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> PutBook(Book book)
        {
            if (_bookService == null)
            {
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////       nie zrobione
        /// <returns></returns>
        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(Book book)
        {
            
            if (_bookService == null)
            {
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_bookService == null)
            {
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
