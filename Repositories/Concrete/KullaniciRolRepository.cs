using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class KullaniciRolRepository : GenericRepository<KullaniciRol>, IKullaniciRolRepository
    {
        private readonly Context _context;
        private readonly DbSet<KullaniciRol> _dbSet;
        public KullaniciRolRepository(Context context) : base(context)
        {
            _context = context;
            _dbSet = _context.KullaniciRols;
        }

    }
}
