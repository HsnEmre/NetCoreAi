using ApiDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-41V1OLM\\SQLEXPRESS;Database=ApiAIDb;Integrated Security=True;TrustServerCertificate=True");
            }
        }



        public DbSet<Customer> Customers { get; set; }
    }
}
