using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using Microsoft.EntityFrameworkCore;

namespace codefirst_deneme.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private Context _context;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(Context context)
        {
               _context=context;
               _dbSet = _context.Set<TEntity>();
        }
        public void Ekle(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task<TEntity?> Getir(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Guncelle(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public async void Sil(int id)
        {
            var entity =await Getir(id);
            if(entity != null) {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            
        }

        public IEnumerable<TEntity> TumunuGetir()
        {
            return _dbSet;
        }
    }
}
