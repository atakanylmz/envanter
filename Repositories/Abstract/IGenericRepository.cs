namespace codefirst_deneme.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> Getir(int id);
        Task<List<TEntity>> TumunuGetir();
        Task Sil(int id);
        Task Guncelle(TEntity entity);    
        Task<TEntity> Ekle(TEntity entity);
    }
}
