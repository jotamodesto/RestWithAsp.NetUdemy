using System.ComponentModel.DataAnnotations;

namespace RestWithAspNETUdemy.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}