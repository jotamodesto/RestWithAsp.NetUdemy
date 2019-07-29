using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestWithAspNETUdemy.Model.Base
{
    //[DataContract]
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}