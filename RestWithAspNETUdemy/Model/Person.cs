using System.ComponentModel.DataAnnotations.Schema;
using RestWithAspNETUdemy.Model.Base;

namespace RestWithAspNETUdemy.Model
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}