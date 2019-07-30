using System.Linq;
using System.Collections.Generic;
using RestWithAspNETUdemy.Data.Converter;
using RestWithAspNETUdemy.Data.VO;
using RestWithAspNETUdemy.Model;

namespace RestWithAspNETUdemy.Data.Converters
{
    public class LoginConverter : IParser<LoginVO, User>, IParser<User, LoginVO>
    {
        public User Parse(LoginVO origin)
        {
            if (origin == null) return new User();
            return new User
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public LoginVO Parse(User origin)
        {
            if (origin == null) return new LoginVO();
            return new LoginVO
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public List<User> ParseList(List<LoginVO> origin)
        {
            if (origin == null) return new List<User>();
            return origin.Select(l => Parse(l)).ToList();
        }

        public List<LoginVO> ParseList(List<User> origin)
        {
            if (origin == null) return new List<LoginVO>();
            return origin.Select(u => Parse(u)).ToList();
        }
    }
}