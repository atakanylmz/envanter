using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        private readonly Context _context;
        private readonly DbSet<Rol> _dbSet;
        public RolRepository(Context context) : base(context)
        {
            _context = context;
            _dbSet = _context.Rols;
        }
     
    }
}
