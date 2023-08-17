using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codefirst_deneme.ModelKonfigurasyon
{
    public class RolKonfigurasyon : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ad).IsRequired();
        }
    }
}
