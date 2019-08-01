using RestWithAspNETUdemy.Data.VO;

namespace RestWithAspNETUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(LoginVO login);
    }
}