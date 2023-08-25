using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class KullaniciRepository : GenericRepository<Kullanici>, IKullaniciRepository
    {
        private readonly Context _context;
        private readonly DbSet<Kullanici> _dbSet;
        public KullaniciRepository(Context context) : base(context)
        {
            _context = context;
            _dbSet=_context.Kullanicis;
        }

        public async Task<Kullanici?> EpostaylaGetirInclude(string eposta)
        {
            return  await _dbSet.Include(k => k.KullaniciRols).ThenInclude(kr => kr.Rol).Where(k => k.Eposta == eposta).FirstOrDefaultAsync();
        }

        public async Task<List<Kullanici>> TumunuGetirInclude()
        {
            return await _dbSet.Include(k => k.KullaniciRols).ThenInclude(kr => kr.Rol).ToListAsync();
        }
        public async Task<Kullanici?> GetirInclude(int id)
        {
            return await _dbSet.Include(k => k.KullaniciRols).ThenInclude(kr => kr.Rol).Where(k => k.Id == id).FirstOrDefaultAsync();
        }
    }
}
