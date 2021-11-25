using Microsoft.EntityFrameworkCore;
using GestionApp.Models;

namespace GestionApp.Models
{
    public class GestionAppContext : DbContext
    {
        public GestionAppContext(DbContextOptions<GestionAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<GestionApp.Models.App> App { get; set; }
        public DbSet<GestionApp.Models.Instalacion> Instalacion { get; set; }
        public DbSet<GestionApp.Models.Operario> Operario { get; set; }
        public DbSet<GestionApp.Models.Sensor> Sensor { get; set; }
        public DbSet<GestionApp.Models.Telefono> Telefono { get; set; }
    }
}
