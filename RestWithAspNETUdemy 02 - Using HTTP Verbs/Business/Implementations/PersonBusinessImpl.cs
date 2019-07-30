using System.Collections.Generic;
using RestWithAspNETUdemy.Data.Converters;
using RestWithAspNETUdemy.Data.VO;
using RestWithAspNETUdemy.Repository;

namespace RestWithAspNETUdemy.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImpl(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id) => _repository.Delete(id);

        public List<PersonVO> FindAll() => _converter.ParseList(_repository.FindAll());

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return FindAll();
            else
            {
                return _converter.ParseList(_repository.FindByName(firstName, lastName));
            }
        }

        public PersonVO FindById(long id) => _converter.Parse(_repository.FindById(id));

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }


    }
}