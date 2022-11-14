using Microsoft.EntityFrameworkCore;
using VoterBot.Entities;
using VoterBot.Models;

namespace VoterBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<BotResponseText> BotResponseTexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DefaultValues.CONNECTION);
        }
    }
}
