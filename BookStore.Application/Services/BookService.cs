using AutoMapper;
using BookStore.Application.DTOs.Books;
using BookStore.Application.Interfaces;
using BookStore.Common.Results;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository repository, IMapper mapper, ILogger<BookService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<BookDto>> CreateAsync(CreateBookDto dto)
        {
            try
            {
                var book = _mapper.Map<Book>(dto);
                await _repository.AddAsync(book);

                _logger.LogInformation("📘 Book created: {Title}", book.Title);

                var result = _mapper.Map<BookDto>(book);
                return Result<BookDto>.Success(result, "Book created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                return Result<BookDto>.Failure("An error occurred while creating the book.");
            }
        }

        public async Task<Result<BookDto>> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null || !book.IsActive)
            {
                _logger.LogWarning("⚠️ Book not found: {Id}", id);
                return Result<BookDto>.Failure("Book not found.");
            }

            var result = _mapper.Map<BookDto>(book);
            return Result<BookDto>.Success(result);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(string? keyword, int page, int pageSize)
        {
            var books = await _repository.SearchAsync(keyword, page, pageSize);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<Result<bool>> UpdateAsync(UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(dto.Id);

            if (book == null || !book.IsActive)
            {
                _logger.LogWarning("⚠️ Book not found for update: {Id}", dto.Id);
                return Result<bool>.Failure("Book not found.");
            }

            _mapper.Map(dto, book);
            book.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(book);
            _logger.LogInformation("🔄 Book updated: {Title}", book.Title);

            return Result<bool>.Success(true, "Book updated successfully.");
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null || !book.IsActive)
            {
                _logger.LogWarning("⚠️ Book not found for deletion: {Id}", id);
                return Result<bool>.Failure("Book not found.");
            }

            book.IsActive = false; // Soft delete
            book.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(book);
            _logger.LogInformation("🗑️ Book soft-deleted: {Title}", book.Title);

            return Result<bool>.Success(true, "Book deleted successfully.");
        }
    }
}
