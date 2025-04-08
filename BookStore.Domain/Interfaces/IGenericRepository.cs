using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    // 'T' là kiểu dữ liệu tổng quát, đảm bảo T phải là một lớp (class)
    public interface IGenericRepository<T> where T : class// Giới hạn T phải là kiểu tham chiếu (class) để Entity Framework có thể làm việc
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
