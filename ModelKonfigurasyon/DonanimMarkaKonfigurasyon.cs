using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codefirst_deneme.ModelKonfigurasyon
{
    public class DonanimMarkaKonfigurasyon : IEntityTypeConfiguration<DonanimMarka>
    {
        public void Configure(EntityTypeBuilder<DonanimMarka> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Ad).IsRequired();
        }
    }
}
