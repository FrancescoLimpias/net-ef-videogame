using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    internal class TournamentContext : DbContext
    {

        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<SoftwareHouse> SoftwareHouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=net-ef-videogame;Integrated Security=True; TrustServerCertificate=True");
        }
    }
}
