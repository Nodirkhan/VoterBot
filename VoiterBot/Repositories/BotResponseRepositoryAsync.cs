using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoterBot.Data;
using VoterBot.Entities;
using VoterBot.Enums;

namespace VoterBot.Repositories
{
    public class BotResponseRepositoryAsync
    {
        public async Task<BotResponseText> FindByCodition(Expression<Func<BotResponseText, bool>> expression)
        {
            var response = new ApplicationDbContext().BotResponseTexts;
            return await response.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<List<BotResponseText>> GetKeyBoard()
        {
            var response = new ApplicationDbContext().BotResponseTexts;
            return  await response.Where(b => b.Type == ResponseTextType.KeyBoard).ToListAsync();
        }
    }
}
