using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Application.DTOs.Users;
using BookStore.Common.Results;

namespace BookStore.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _repoMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();

            _service = new UserService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        #region RegisterAsync

        [Fact]
        public async Task Register_ShouldFail_WhenEmailExists()
        {
            var dto = new RegisterDto { Email = "test@gmail.com", Password = "123", Name = "Test" };
            _repoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(new User());

            var result = await _service.RegisterAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email đã được sử dụng", result.Message);
        }

        [Fact]
        public async Task Register_ShouldSucceed_WhenEmailNotExists()
        {
            var dto = new RegisterDto { Email = "new@gmail.com", Password = "123", Name = "New" };
            var userEntity = new User { Email = dto.Email, Name = dto.Name };
            var userDto = new UserDto { Email = dto.Email, Name = dto.Name };

            _repoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);
            _mapperMock.Setup(m => m.Map<User>(dto)).Returns(userEntity);
            _mapperMock.Setup(m => m.Map<UserDto>(userEntity)).Returns(userDto);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(userEntity); // ✅ userEntity là User


            var result = await _service.RegisterAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Email, result.Data?.Email);
        }

        #endregion

        #region LoginAsync

        [Fact]
        public async Task Login_ShouldFail_WhenUserNotFound()
        {
            var dto = new LoginDto { Email = "notfound@gmail.com", Password = "123" };
            _repoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);

            var result = await _service.LoginAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email hoặc mật khẩu không đúng", result.Message);
        }

        [Fact]
        public async Task Login_ShouldFail_WhenWrongPassword()
        {
            var dto = new LoginDto { Email = "user@gmail.com", Password = "wrong" };
            var user = new User { Email = dto.Email, PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("correct")) };

            _repoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(user);

            var result = await _service.LoginAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email hoặc mật khẩu không đúng", result.Message);
        }

        [Fact]
        public async Task Login_ShouldSucceed_WhenCredentialsCorrect()
        {
            var password = "123";
            var hash = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            var user = new User { Email = "user@gmail.com", PasswordHash = hash, IsActive = true };
            var dto = new LoginDto { Email = user.Email, Password = password };
            var userDto = new UserDto { Email = user.Email };

            _repoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            var result = await _service.LoginAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Email, result.Data?.Email);
        }

        #endregion

        #region GetByIdAsync

        [Fact]
        public async Task GetById_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { Id = 1, Email = "a@gmail.com", IsActive = true };
            var dto = new UserDto { Id = 1, Email = "a@gmail.com" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(dto);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal("a@gmail.com", result?.Email);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenUserNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((User?)null);

            var result = await _service.GetByIdAsync(2);

            Assert.Null(result);
        }

        #endregion
    }
}