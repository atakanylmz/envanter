using codefirst_deneme.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace codefirst_deneme.ModelKonfigurasyon
{
    public class KullaniciRolKonfigurasyon : IEntityTypeConfiguration<KullaniciRol>
    {
        public void Configure(EntityTypeBuilder<KullaniciRol> builder)
        {
            builder.HasKey(x => new { x.RolId, x.KullaniciId });
            builder.HasOne(x=>x.Kullanici).WithMany(k=>k.KullaniciRols).HasForeignKey(x=>x.KullaniciId);
            builder.HasOne(x => x.Rol).WithMany(r => r.KullaniciRols).HasForeignKey(x => x.RolId);
        }
    }
}
