using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.Dtos.User;
using Core.Utilities.Security.Jwt;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userRegisterDto, string password);
        IDataResult<User> Login(UserLoginDto userLoginDto);

        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult PasswordChange(string password, string oldPassword);
    }
}
