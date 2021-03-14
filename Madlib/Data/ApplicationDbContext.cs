using Madlib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Madlib.Models.Madlib> Madlib { get; set; }
        public DbSet<MadlibBlank> MadlibBlank { get; set; }
        public DbSet<SinglePlayerGame> SinglePlayerGame { get; set; }
        public DbSet<SinglePlayerGameFilledBlank> SinglePlayerGameFilledBlank { get; set; }
    }
}
