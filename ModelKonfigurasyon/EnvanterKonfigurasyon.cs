using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codefirst_deneme.ModelKonfigurasyon
{
    public class EnvanterKonfigurasyon : IEntityTypeConfiguration<Envanter>
    {
        public void Configure(EntityTypeBuilder<Envanter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ad).IsRequired();
            builder.HasOne(x => x.EkleyenKullanici).WithMany(k => k.Envanters).HasForeignKey(x => x.EkleyenId);
        }
    }
}
