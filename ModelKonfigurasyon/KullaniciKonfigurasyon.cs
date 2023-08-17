using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codefirst_deneme.ModelKonfigurasyon
{
    public class KullaniciKonfigurasyon : IEntityTypeConfiguration<Kullanici>
    {
   
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ad).IsRequired();
            builder.Property(x=>x.Soyad).IsRequired();
            builder.Property(x=>x.Eposta).IsRequired();
        }
    }
}
