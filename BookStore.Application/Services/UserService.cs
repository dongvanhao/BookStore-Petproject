using AutoMapper;
using BookStore.Application.DTOs.Users;
using BookStore.Application.Interfaces;
using BookStore.Common.Results;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository repository, IMapper mapper, ILogger<UserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<UserDto>> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return Result<UserDto>.Failure("Email đã được sử dụng");

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = HashPassword(dto.Password);

            await _repository.AddAsync(user);
            _logger.LogInformation("✅ Đăng ký người dùng thành công: {Email}", user.Email);

            return Result<UserDto>.Success(_mapper.Map<UserDto>(user), "Đăng ký thành công");
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return Result<UserDto>.Failure("Email hoặc mật khẩu không đúng");

            if (!user.IsActive)
                return Result<UserDto>.Failure("Tài khoản đang bị khóa");

            _logger.LogInformation("🔐 Đăng nhập thành công: {Email}", user.Email);

            return Result<UserDto>.Success(_mapper.Map<UserDto>(user), "Đăng nhập thành công");
        }
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null || !user.IsActive) return null;

            return _mapper.Map<UserDto>(user);
        }

        // 🔐 Mã hóa mật khẩu đơn giản (hash SHA256)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
