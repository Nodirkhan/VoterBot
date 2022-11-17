using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using VoterBot.Entities;
using VoterBot.Models;

namespace VoterBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BotResponseText> BotResponseTexts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DefaultValues.CONNECTION);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.VoterNumber)
             .HasIdentityOptions(startValue: 1000);

        }
    }
}
