using codefirst_deneme.ModelKonfigurasyon;
using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace codefirst_deneme.Repositories.Abstract.EfCore
{
    public class Context : DbContext
    {
        public Context() : base()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Rol>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //});
            modelBuilder.ApplyConfiguration(new KullaniciKonfigurasyon());
            modelBuilder.ApplyConfiguration(new RolKonfigurasyon());
            modelBuilder.ApplyConfiguration(new EnvanterKonfigurasyon());
            modelBuilder.ApplyConfiguration(new KullaniciRolKonfigurasyon());
            modelBuilder.ApplyConfiguration(new DonanimMarkaKonfigurasyon());
        }


        public DbSet<Rol> Rols { get; set; }

        public DbSet<Kullanici> Kullanicis { get; set; }

        public DbSet<Envanter> Envanters { get; set; }

        public DbSet<KullaniciRol> KullaniciRols { get; set; }

        public DbSet<DonanimMarka> DonanimMarkas { get; set; }


    }
}
