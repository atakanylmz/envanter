using codefirst_deneme.Models;

namespace codefirst_deneme.Repositories.Abstract
{
    public interface IKullaniciRepository:IGenericRepository<Kullanici>
    {
        Task<Kullanici?> EpostaylaGetirInclude(string eposta);
        Task<Kullanici?> GetirInclude(int Id);
        Task<List<Kullanici>> TumunuGetirInclude();
    }
}
