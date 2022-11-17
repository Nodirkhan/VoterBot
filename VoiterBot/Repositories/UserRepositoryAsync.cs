using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VoterBot.Data;
using VoterBot.Entities;

namespace VoterBot.Repositories
{
    public class UserRepositoryAsync
    {

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                var context = new ApplicationDbContext();
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetById(long userId)
        {
            var users = new ApplicationDbContext().Users;
            return await users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task Update(User user)
        {
            var context = new ApplicationDbContext();
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();  
        }

    }
}
