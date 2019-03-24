using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PokerCore.Model.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DBGame> Games { get; set; }
        public DbSet<DBPlayer> Players { get; set; }
        public DbSet<DBTableCards> TableCards { get; set; }
        public DbSet<DBRound> Rounds { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pokerdb;Trusted_Connection=True;");
        }
    }
}
