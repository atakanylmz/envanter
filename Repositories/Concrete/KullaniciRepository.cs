using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class KullaniciRepository : GenericRepository<Kullanici>, IKullaniciRepository
    {
        private Context _context;
        private DbSet<Kullanici> _dbSet;
        public KullaniciRepository(Context context) : base(context)
        {
            _context = context;
            _dbSet=_context.Kullanicis;
        }

        public Task<Kullanici?> EpostaylaGetirInclude(string eposta)
        {
            return _dbSet.Include(k => k.KullaniciRols).ThenInclude(kr => kr.Rol).Where(k => k.Eposta == eposta).FirstOrDefaultAsync();
        }











    }
}
