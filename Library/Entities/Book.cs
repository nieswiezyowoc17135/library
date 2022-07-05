using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        [MaxLength(9)]
        public string Isbn { get; set; }
    }
}
