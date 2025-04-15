using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Application.DTOs.Books;
using BookStore.Common.Results;

namespace BookStore.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BookService>> _loggerMock;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _repoMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BookService>>();

            _service = new BookService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        #region GetByIdAsync

        [Fact]
        public async Task GetById_ShouldReturnSuccess_WhenBookExists()
        {
            var book = new Book { Id = 1, Title = "Test", IsActive = true };
            var dto = new BookDto { Id = 1, Title = "Test" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookDto>(book)).Returns(dto);

            var result = await _service.GetByIdAsync(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Test", result.Data?.Title);
        }

        [Fact]
        public async Task GetById_ShouldReturnFailure_WhenBookNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Book?)null);

            var result = await _service.GetByIdAsync(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found.", result.Message);
        }

        #endregion

        #region CreateAsync

        [Fact]
        public async Task Create_ShouldReturnSuccess_WhenValidData()
        {
            var createDto = new CreateBookDto { Title = "New Book" };
            var entity = new Book { Title = "New Book" };
            var dto = new BookDto { Title = "New Book" };

            _mapperMock.Setup(m => m.Map<Book>(createDto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<BookDto>(entity)).Returns(dto);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync(entity);

            var result = await _service.CreateAsync(createDto);

            Assert.True(result.IsSuccess);
            Assert.Equal("New Book", result.Data?.Title);
        }

        [Fact]
        public async Task Create_ShouldReturnFailure_WhenExceptionThrown()
        {
            var createDto = new CreateBookDto { Title = "Err" };

            _mapperMock.Setup(m => m.Map<Book>(createDto)).Throws(new Exception("error"));

            var result = await _service.CreateAsync(createDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while creating the book.", result.Message);
        }

        #endregion

        #region UpdateAsync

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenBookExists()
        {
            var updateDto = new UpdateBookDto { Id = 1, Title = "Updated" };
            var book = new Book { Id = 1, Title = "Old", IsActive = true };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map(updateDto, book)).Verifiable();

            var result = await _service.UpdateAsync(updateDto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Update_ShouldReturnFailure_WhenBookNotFound()
        {
            var updateDto = new UpdateBookDto { Id = 99 };

            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);

            var result = await _service.UpdateAsync(updateDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found.", result.Message);
        }

        #endregion

        #region DeleteAsync

        [Fact]
        public async Task Delete_ShouldReturnSuccess_WhenBookExists()
        {
            var book = new Book { Id = 1, Title = "To Delete", IsActive = true };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _repoMock.Setup(r => r.UpdateAsync(book)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Delete_ShouldReturnFailure_WhenBookNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);

            var result = await _service.DeleteAsync(99);

            Assert.False(result.IsSuccess);
            Assert.Equal("Book not found.", result.Message);
        }

        #endregion

        #region GetAllAsync

        [Fact]
        public async Task GetAll_ShouldReturnListOfBooks()
        {
            var books = new List<Book> { new Book { Id = 1, Title = "A" } };
            var dtos = new List<BookDto> { new BookDto { Id = 1, Title = "A" } };

            _repoMock.Setup(r => r.SearchAsync(null, 1, 10)).ReturnsAsync(books);
            _mapperMock.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(dtos);

            var result = await _service.GetAllAsync(null, 1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        #endregion
    }
}