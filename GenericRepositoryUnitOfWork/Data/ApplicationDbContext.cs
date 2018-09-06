using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepositoryUnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryUnitOfWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        //All database override will be apply here
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
        }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        
    }
}
