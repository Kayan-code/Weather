using Microsoft.EntityFrameworkCore;
using testeaec.Models;

namespace Weather.Context
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<LogResponse> LogEntries { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Airport>().ToTable("Airports");
            modelBuilder.Entity<LogResponse>().ToTable("LogEntries");
            modelBuilder.Entity<WeatherCondition>().ToTable("WeatherCondition");

            // Especificar o tipo de coluna para a propriedade ElapsedMilliseconds
            modelBuilder.Entity<LogResponse>()
                .Property(l => l.ElapsedMilliseconds)
                .HasColumnType("decimal(10,2)");

            // Configurar relação entre WeatherCondition e City
            modelBuilder.Entity<WeatherCondition>()
                .HasOne(wc => wc.City)
                .WithMany(c => c.Conditions)
                .HasForeignKey(wc => wc.CityId);
        }
    }
}

