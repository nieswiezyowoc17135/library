using CsvHelper;
using Library.Entities;
using Library.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly MyBooksContext _context;

        public FileController(MyBooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetData()
        {
            //pobrane dane z bazdy danych i wjebane do listy
            List<BookDto> downloadedData = new List<BookDto>();
            downloadedData = await _context.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Author = x.Author,
                Isbn = x.Isbn
            }).ToListAsync();

            //ten path jest do zmiany na roznych systemach
            using (var writer = new StreamWriter("C:\\Users\\pswider\\Downloads\\downloadedDataFile.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(downloadedData);
            }
            return Ok(downloadedData);
        }
    }
}
