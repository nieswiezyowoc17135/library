using CsvHelper;
using CsvHelper.Configuration;
using Library.Entities;
using Library.Services.Interfaces;
using Library.Services.Models;
using Microsoft.FeatureManagement;
using System.Globalization;

namespace Library.Services
{
    public class FileService : IFileService
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;
        private readonly IFeatureManager _featureManager;        

        public FileService(IBookService bookService, IEmailService emailService, IFeatureManager featureManager)
        {
            _bookService = bookService;
            _emailService = emailService;
            _featureManager = featureManager;
        }

        public async Task<byte[]> CreatingFile(int take, int skip, string word)
        {
            //pobrane dane z bazdy danych i wjebanie do listy
            List<BookDto> downloadedData = new List<BookDto>();
            downloadedData = await _bookService.FilterBooks(take, skip, word);

            //ustawianie konfigu odnosnie formatowania
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };

            //plik do którego będą zapisywane dane (juz nie plik bo bedzie uzywany memoryStream)
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))

            //uzywanie klasy CSVwritera do ustawienia poprawnych ustawien i zapisanie danych pobranych do pliku downloadedDataFile z dowloadedData param.
            using (var csv = new CsvWriter(writer, config))
            {
                //zapisywanie danych do downloadedDataFile.csv z downloadedData
                csv.WriteRecords(downloadedData);
            }

            //zwracanie danych do pliku o określonej nazwie 
            return memoryStream.ToArray();
        }

        public async Task<bool> AddingToDatabase(IFormFile filePath)
        {
            //sprawdzanie czy jest włączone wysyłanie maila
            Boolean isSendingEnabled = await _featureManager.IsEnabledAsync("isSendingEnabled");

            //config jak ma czytac plik
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) {Delimiter = ";"};

            //zmienna sprawdzajaca czy plik jest o rozszerzeniu csv
            string lastChars = "";
            lastChars = filePath.FileName.Substring(filePath.FileName.Length-3);

            //warunek
            if (lastChars == "csv")
            {
                //deklaracja potrzebnych zmiennych
                List<BookDto> recordsFromFile = null;
                Book bookToDb = new Book();

                //wjebywanie pliku do listy recordsFromFile
                using (var reader = new StreamReader(filePath.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    recordsFromFile = csv.GetRecords<BookDto>().ToList();
                }

                foreach (var book in recordsFromFile)
                {
                    await _bookService.AddSomeBooks(book);
                }

                //wysylanie jezeli jest włączone
                if (isSendingEnabled)
                {
                    _emailService.SendEmail();
                }
                return true;
            } else
            {
                return false;
            }
        }
    }
}
