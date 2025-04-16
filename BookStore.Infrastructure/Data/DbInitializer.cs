using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Seed
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any() || context.Books.Any() || context.Orders.Any())
                return;

            // USERS
            var users = new List<User>();
            for (int i = 1; i <= 5; i++)
            {
                users.Add(new User
                {
                    Name = $"User {i}",
                    Email = $"user{i}@mail.com",
                    PasswordHash = "123456",
                    Role = "User",
                    Phone = $"012345678{i}",
                    Address = $"Địa chỉ {i}",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                });
            }
            context.Users.AddRange(users);
            context.SaveChanges(); // 👈 Phải SaveChanges để lấy User.Id sau này

            // BOOKS
            var books = new List<Book>();
            for (int i = 1; i <= 10; i++)
            {
                books.Add(new Book
                {
                    Title = $"Book {i}",
                    Author = $"Author {i}",
                    Price = 100000 + i * 10000,
                    Stock = 50,
                    ISBN = $"ISBN-{1000 + i}",
                    Description = $"Mô tả sách {i}",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                });
            }
            context.Books.AddRange(books);
            context.SaveChanges(); // 👈 Cũng phải save để có Book.Id

            // CARTS + CART ITEMS
            var carts = new List<Cart>();
            var cartItems = new List<CartItem>();
            for (int i = 0; i < users.Count; i++)
            {
                var cart = new Cart
                {
                    UserId = users[i].Id
                };
                context.Carts.Add(cart);
                context.SaveChanges(); // 👈 cần save để có cart.Id

                cartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    BookId = books[i].Id,
                    Quantity = 2
                });
            }
            context.CartItems.AddRange(cartItems);
            context.SaveChanges();

            // ORDERS + ORDER ITEMS
            var orders = new List<Order>();
            var orderItems = new List<OrderItem>();
            for (int i = 0; i < users.Count; i++)
            {
                var order = new Order
                {
                    UserId = users[i].Id,
                    OrderDate = DateTime.Now.AddDays(-i)
                };
                context.Orders.Add(order);
                context.SaveChanges();

                orderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    BookId = books[i].Id,
                    Quantity = 1,
                    Price = books[i].Price
                });
            }
            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();
        }
    }
}
