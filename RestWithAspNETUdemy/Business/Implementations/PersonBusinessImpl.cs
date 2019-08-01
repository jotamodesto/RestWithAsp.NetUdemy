using System.Collections.Generic;
using System.Text;
using RestWithAspNETUdemy.Data.Converters;
using RestWithAspNETUdemy.Data.VO;
using RestWithAspNETUdemy.Repository;
using Tapioca.HATEOAS.Utils;

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

        public PagedSearchDTO<PersonVO> PagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            var queryBuilder = new StringBuilder(@"select * from `persons` p where 1 = 1 ");
            if (!string.IsNullOrWhiteSpace(name)) queryBuilder.Append($"and p.FirstName like '%{name}%' ");
            queryBuilder.Append($"order by p.FirstName {sortDirection} limit {pageSize} offset {page}");

            var countBuilder = new StringBuilder(@"select count(*) from `persons` p where 1 = 1 ");
            if (!string.IsNullOrWhiteSpace(name)) countBuilder.Append($"and p.FirstName like '%{name}%' ");

            var persons = _repository.PagedSearch(queryBuilder.ToString());
            int totalResults = _repository.GetCount(countBuilder.ToString());

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = _converter.ParseList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }
}