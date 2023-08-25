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
        public async Task<TEntity> Ekle(TEntity entity)
        {
             _dbSet.Add(entity);
             await  _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Getir(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Guncelle(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Sil(int id)
        {
            var entity =await Getir(id);
            if(entity != null) {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task TopluEkle(List<TEntity> entities)
        {
            _dbSet.AddRange (entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> TumunuGetir()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
