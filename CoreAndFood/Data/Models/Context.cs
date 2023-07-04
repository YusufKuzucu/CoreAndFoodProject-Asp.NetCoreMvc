using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoreAndFood.Data.Models
{
    public class Context:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
		public DbSet<Admin> Admins { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("??");
        }
    }
}
