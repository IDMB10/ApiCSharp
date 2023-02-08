using APIWeb;
using APIWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
    }
}