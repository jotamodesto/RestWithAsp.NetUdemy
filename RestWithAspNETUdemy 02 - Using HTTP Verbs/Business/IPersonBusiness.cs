using System.Collections.Generic;
using RestWithAspNETUdemy.Data.VO;

namespace RestWithAspNETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        List<PersonVO> FindByName(string firstName, string lastName);
        PersonVO Update(PersonVO person);
        void Delete(long id);

    }
}