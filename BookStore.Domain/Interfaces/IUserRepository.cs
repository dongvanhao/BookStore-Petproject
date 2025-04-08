using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);//Tim Nguoi DUng Theo Email
        Task<bool> CheckEmailExistAsync(string email);//Kiem Tra Email Da Ton Tai Chua
    }
}
