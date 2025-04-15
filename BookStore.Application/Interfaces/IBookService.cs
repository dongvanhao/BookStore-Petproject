using BookStore.Application.DTOs.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Common.Results;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(string? keyword, int page, int pageSize);
        Task<Result<BookDto>> GetByIdAsync(int id);
        Task<Result<BookDto>> CreateAsync(CreateBookDto bookDto);
        Task<Result<bool>> UpdateAsync(UpdateBookDto bookDto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
