using System;
using System.Runtime.Serialization;

namespace RestWithAspNETUdemy.Data.VO
{
    [DataContract]
    public class BookVO
    {
        [DataMember(Order = 1)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}