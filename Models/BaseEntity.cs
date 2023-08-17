using System.ComponentModel.DataAnnotations;

namespace codefirst_deneme.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime EklemeTarihi { get; set; }
    }
}
