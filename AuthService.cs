using DocumentReaderApp.Models;
using System.Linq;
using System.Threading.Tasks;
using DocumentReaderApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentReaderApp.Services
{
    public class AuthService
    {
        private readonly DatabaseContext _context;

        public AuthService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email)) return false;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null && user.VerifyPassword(password);
        }
    }
}
