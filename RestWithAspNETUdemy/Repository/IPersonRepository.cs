using System.Collections.Generic;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Repository.Generic;

namespace RestWithAspNETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}