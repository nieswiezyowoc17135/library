namespace Library.Services.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Copies => 10;
    }
}
