using AutoMapper;
using BookStore.Application.DTOs.Books;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class BookServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_WhenValidInput()
        {
            // Arrange
            var dto = new CreateBookDto
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Price = 150000,
                Stock = 100,
                Description = "Book for professional developers",
                ISBN = "9780132350884",
                ImageUrl = "https://image.com/clean-code.jpg"
            };

            var mappedBook = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description,
                ISBN = dto.ISBN,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };

            var mappedDto = new BookDto
            {
                Title = dto.Title,
                Author = dto.Author,
                Price = dto.Price
            };

            var repoMock = new Mock<IBookRepository>();
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<BookService>>();

            // Cấu hình AutoMapper giả
            mapperMock.Setup(m => m.Map<Book>(dto)).Returns(mappedBook);
            mapperMock.Setup(m => m.Map<BookDto>(It.IsAny<Book>())).Returns(mappedDto);

            // Cấu hình repository giả
            repoMock.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync(mappedBook);

            var service = new BookService(repoMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Clean Code", result.Data?.Title);
            Assert.Equal(dto.Author, result.Data?.Author);
        }
        


    }
}
