using System.Linq;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Model.Context;

namespace RestWithAspNETUdemy.Repository.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private MySQLContext _context;

        public UserRepositoryImpl(MySQLContext context) => _context = context;

        public User FindByLogin(string login) => _context.Users.SingleOrDefault(u => u.Login.Equals(login));
    }
}