using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoterBot.Data;
using VoterBot.Entities;

namespace VoterBot.Repositories
{
    public class BotResponseRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BotResponseText> _responses;

        public BotResponseRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _responses = _context.BotResponseTexts;
        }

        public async Task<BotResponseText> FindByCodition(Expression<Func<BotResponseText, bool>> expression)=>
            await _responses.AsNoTracking().FirstOrDefaultAsync(expression);
    }
}
