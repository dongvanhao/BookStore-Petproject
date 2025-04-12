using AutoMapper;
using BookStore.Application.DTOs.Books;
using BookStore.Application.DTOs.Carts;
using BookStore.Application.DTOs.Orders;
using BookStore.Application.DTOs.Users;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book ↔ BookDto
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();

            // User ↔ UserDto
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // xử lý hashing riêng


            // Cart ↔ CartDto
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price));

            // Order ↔ OrderDto
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}
