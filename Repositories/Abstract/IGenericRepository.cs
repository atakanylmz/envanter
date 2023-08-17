namespace codefirst_deneme.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> Getir(int id);
        IEnumerable<TEntity> TumunuGetir();
        void Sil(int id);
        void Guncelle(TEntity entity);    
        void Ekle(TEntity entity);
    }
}
