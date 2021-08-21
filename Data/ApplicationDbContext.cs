using Microsoft.EntityFrameworkCore;
using PessengerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PessengerApp.Extensions;

namespace PessengerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Pessenger> Pessengers { get; set; }
    }
}
