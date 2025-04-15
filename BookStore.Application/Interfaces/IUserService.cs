using BookStore.Application.DTOs.Users;
using BookStore.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDto>> RegisterAsync(RegisterDto dto);
        Task<Result<UserDto>> LoginAsync(LoginDto dto);
        Task<UserDto?> GetByIdAsync(int id);
    }
}
