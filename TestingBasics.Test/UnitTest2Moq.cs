using Library.Controllers;
using Library.Services.Interfaces;
using Library.Services.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace TestingBasics.Test
{
    public class UnitTest2Moq
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly BooksController _controller;

        //konstruktor na controller z serwisami
        public UnitTest2Moq()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BooksController(_mockBookService.Object);
        }

        [Fact]
        public async void GettingBooks()
        {
            //ustawianie co serwis ma zwrocic w przypadku wywolania metody pobrania wszystkich ksiazek
            _mockBookService.Setup(repo => repo.GetAllBooks())
                .ReturnsAsync(new List<BookDto>() {new BookDto()
                {
                    Id = 1,
                    Author = "Stasiek",
                    Isbn = "123456789"
                }, new BookDto()
                {
                    Id = 2,
                    Author = "Jasiek",
                    Isbn = "987654321"
                }});

            //reult to lista ksiazek ktore podalismy w setupie do metody GetAllBooks
            var result = await _controller.GetBooks();

            result.Value.Should().BeEquivalentTo(await _mockBookService.Object.GetAllBooks());
        }

        [Fact]
        public async void GetingOneBook()
        {
            int id = 3;

            _mockBookService.Setup(repo => repo.GetOneBook(id))
                .ReturnsAsync(new BookDto()
                {
                    Id = 3,
                    Author = "John",
                    Isbn = "123567980"
                });

            var result = await _controller.GetBook(id);

            result.Value.Should().BeEquivalentTo(await _mockBookService.Object.GetOneBook(id));
        }

        [Fact]
        public async void AddingBook()
        {
            BookDto newBook = new BookDto()
            {
                Id = 1,
                Author = "Janek",
                Isbn = "555544442"
            };

            _mockBookService.Setup(repo => repo.AddSomeBooks(newBook))
                .ReturnsAsync(true);

            var result = await _controller.PostBook(newBook);

            result.Should().NotBe(false);
        }

        [Fact]
        public async void DeletingBook()
        {
            int id = 1;

            _mockBookService.Setup(repo => repo.DeleteBook(id))
                .ReturnsAsync(true);

            var result = await _controller.DeleteBook(id);

            result.Should().NotBe(false);
        }

        [Fact]
        public async void EditingBook()
        {
            //potrzebne dane
            int id = 1;
            BookDto bookWithNewData = new BookDto()
            {
                Id = 1,
                Author = "Pawel",
                Isbn = "123789456"
            };

            _mockBookService.Setup(repo => repo.EditBook(id, bookWithNewData))
                .ReturnsAsync(true);

            var result = await _controller.PutBook(bookWithNewData, id);

            result.Should().NotBe(false);
        }
    }
}
