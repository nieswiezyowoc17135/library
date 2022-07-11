using CsvHelper;
using CsvHelper.Configuration;
using Library.Entities;
using Library.Services.Interfaces;
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

        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult> GetDataFromDb()
        {
            var bytes = await _fileService.CreatingFile();
            if (bytes != null)
            {
                //zwracanie danych do pliku o określonej nazwie 
                return File(bytes, "application/json", Path.GetFileName("Data.csv"));
            } 
            //to nie dziala bo bytes bedzie zawsze zapelnione headerami
            else
            {
                return NoContent();
            }            
       }

        [HttpPost]
        public async Task<ActionResult> PostDataToDb(IFormFile filePath)
        {
            if (await _fileService.AddingToDatabase(filePath))
            {
                return Ok();
            } else
            {
                return NoContent();
            }
        }
    }
}
