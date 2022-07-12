using Library.Services.Models;

namespace Library.Services.Interfaces
{
    //interfejs, który definiuje jak ma wyglądać klasa BookService
    public interface IBookService
    {
        Task<bool> AddSomeBooks(BookDto book);
        Task<bool> DeleteBook(int id);
        Task<bool> EditBook(int id, BookDto book);
        Task<BookDto> GetOneBook(int id);                
        Task<List<BookDto>> GetAllBooks();
        Task<List<BookDto>> FilterBooks(int take, int skip, string word);
    }
}
