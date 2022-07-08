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
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks()
        {
            return await _bookService.GetAllBooks();
        }

        /// /////////////////////////////////////////////////////////////////////////////////      Zrobione
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            /*var book = await _bookService.GetOneBook(id);*/
            if (_bookService == null)
            {
                return NotFound();
            }
            else
            {
                return await _bookService.GetOneBook(id); 
            }

        }

        // /////////////////////////////////////////////////////////////////////////////////      Zrobione
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> PutBook(BookDto book, int id)
        {
            if (_bookService == null)
            {
                return NotFound();
            }

            if (await _bookService.EditBook(id, book))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////      Zrobione
        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookDto book)
        {

            if (await _bookService.AddSomeBooks(book))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        /// ///////////////////////////////////////////////////////////////////////////////////      Zrobione
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_bookService == null)
            {
                return NotFound();
            }
            if (await _bookService.DeleteBook(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
