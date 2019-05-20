using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFLCore.Models;

namespace NFLCore.Data
{
    public class NflContext : DbContext
    {
        public NflContext(DbContextOptions<NflContext> options)
            : base(options)
        {
            //
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
