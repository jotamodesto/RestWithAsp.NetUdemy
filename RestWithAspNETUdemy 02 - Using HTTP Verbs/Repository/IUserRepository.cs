using System.Collections.Generic;
using RestWithAspNETUdemy.Model;

namespace RestWithAspNETUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}