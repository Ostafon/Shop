using System;
using System.Linq;
using Romofyi.Application;

namespace WebApplication1.Application
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public Customer AuthenticateUser(string username, string password)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Username == username);
            if (user != null && _passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public bool HasRequiredRole(Customer user, string role)
        {
            return user.Role.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        public Customer RegisterUser(string username, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(password);

            var newUser = new Customer
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = "User"
            };

            _context.Customers.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}