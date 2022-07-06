using Library.Services.Models;

namespace Library.Services.Interfaces
{
    //interfejs, który definiuje jak ma wyglądać klasa BookService
    public interface IBookService
    {
        Task<long> AddSomeBooks(BookDto book);
        Task DeleteBook(BookDto book);
        Task EditBook(BookDto book);
        Task<BookDto> GetBook(int id);                
        Task<List<BookDto>> GetBooks();                
    }
}
