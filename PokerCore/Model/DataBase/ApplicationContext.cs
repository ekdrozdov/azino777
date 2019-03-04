using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PokerCore.Model.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DBGame> Games { get; set; }
        public DbSet<DBTableCards> TableCards { get; set; }
        public DbSet<DBRound> Rounds { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBPlayerGame>()
                .HasKey(t => new { t.DBPlayerId, t.DBGameId });

            modelBuilder.Entity<DBPlayerGame>()
                .HasOne(sc => sc.Player)
                .WithMany(s => s.PlayersGames)
                .HasForeignKey(sc => sc.DBPlayerId);

            modelBuilder.Entity<DBPlayerGame>()
                .HasOne(sc => sc.Game)
                .WithMany(c => c.PlayersGames)
                .HasForeignKey(sc => sc.DBGameId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pokerdb;Trusted_Connection=True;");
        }
    }
}
