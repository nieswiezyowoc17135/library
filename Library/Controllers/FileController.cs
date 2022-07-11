using CsvHelper;
using CsvHelper.Configuration;
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
                        
            //ustawianie konfigu odnosnie formatowania
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";"};
            
            //plik do którego będą zapisywane dane
            using (var writer = new StreamWriter("downloadedDataFile.csv"))
            //uzywanie klasy CSVwritera do ustawienia poprawnych ustawien i zapisanie danych pobranych do pliku downloadedDataFile z dowloadedData param.
            using (var csv = new CsvWriter(writer, config))
            {
                //zapisywanie danych do downloadedDataFile.csv z downloadedData
                csv.WriteRecords(downloadedData);
            }

            //tworzenie pliku fizycznego, który będziemy zwracać
            var bytes = await System.IO.File.ReadAllBytesAsync("downloadedDataFile.csv");

            /*return Ok(downloadedData);*/
            return File(bytes, "application/json", Path.GetFileName("Data.csv"));
        }
    }
}
