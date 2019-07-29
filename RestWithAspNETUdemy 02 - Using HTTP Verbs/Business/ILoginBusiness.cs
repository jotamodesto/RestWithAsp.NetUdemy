using System.Collections.Generic;
using RestWithAspNETUdemy.Model;

namespace RestWithAspNETUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(User login);
    }
}