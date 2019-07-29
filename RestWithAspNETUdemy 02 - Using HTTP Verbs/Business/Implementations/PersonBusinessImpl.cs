using System.Collections.Generic;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Repository.Generic;

namespace RestWithAspNETUdemy.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IRepository<Person> _repository;

        public PersonBusinessImpl(IRepository<Person> repository) => _repository = repository;

        public Person Create(Person person) => _repository.Create(person);

        public void Delete(long id) => _repository.Delete(id);

        public List<Person> FindAll() => _repository.FindAll();

        public Person FindById(long id) => _repository.FindById(id);

        public Person Update(Person person) => _repository.Update(person);
    }
}