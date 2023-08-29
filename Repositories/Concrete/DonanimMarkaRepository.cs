using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class DonanimMarkaRepository : GenericRepository<DonanimMarka>, IDonanimMarkaRepository
    {
        private readonly Context _context;
        private readonly DbSet<DonanimMarka> _dbSet;
        public DonanimMarkaRepository(Context context) : base(context)
        {
            _context = context;
            _dbSet = _context.DonanimMarkas;
        }



    }
}
