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


        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            if (_bookService == null)
            {
                return NotFound();
            } else
            {
                return NotFound();
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
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

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
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
