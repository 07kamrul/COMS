using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ErrorDbContext : DbContext
    {
        IConfiguration _Config;
        public ErrorDbContext(IConfiguration config)
        {
            _Config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_Config.GetConnectionString("ErrorDBConnection"));
        }
        public virtual DbSet<Error> Errors { get; set; }
    }
}
