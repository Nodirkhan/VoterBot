using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VoterBot.Data;
using VoterBot.Entities;

namespace VoterBot.Repositories
{
    public class UserRepositoryAsync
    {
        private ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepositoryAsync(ApplicationDbContext context)
        {
            _context  = context ?? new ApplicationDbContext();
            _users = context.Users;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                await _users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetById(long userId) =>
            await _users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();  
        }

    }
}
