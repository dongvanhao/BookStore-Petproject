using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> SearchAsync(string ? keyword, int page, int pageSize);
        Task<int> CountAsync(string? keyword);
    }
}
