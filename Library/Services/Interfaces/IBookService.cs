using Library.Services.Models;

namespace Library.Services.Interfaces
{
    //interfejs, który definiuje jak ma wyglądać klasa BookService
    public interface IBookService
    {
        Task AddSomeBooks(BookDto book);
        Task DeleteBook(BookDto book);
        Task EditBook(BookDto book);
        Task<BookDto> GetOneBook(int id);                
        Task<List<BookDto>> GetAllBooks();                
    }
}
