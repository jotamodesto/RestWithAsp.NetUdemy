using System.Collections.Generic;
using System.Linq;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Model.Context;
using RestWithAspNETUdemy.Repository.Generic;

namespace RestWithAspNETUdemy.Repository.Implementations
{
    public class PersonRepositoryImpl : Repository<Person>, IPersonRepository
    {
        public PersonRepositoryImpl(MySQLContext context) : base(context) { }

        public List<Person> FindByName(string firstName, string lastName) =>
            _context.Persons.Where(p => p.FirstName.Contains(firstName) || p.LastName.Contains(lastName)).ToList();
    }
}