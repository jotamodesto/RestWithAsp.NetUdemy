
using System.Runtime.Serialization;

namespace RestWithAspNETUdemy.Data.VO
{
    [DataContract]
    public class PersonVO
    {
        [DataMember(Order = 1)]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}