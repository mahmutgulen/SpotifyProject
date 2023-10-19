
using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos.User;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.WebRequestMethods;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserDal _userDal;
        private IUserRoleDal _userRoleDal;
        private IFavoriteDal _favoriteDal;
        private IPlayListDal _playListDal;
        private ILibraryDal _libraryDal;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal, IUserRoleDal userRoleDal, IFavoriteDal favoriteDal, IPlayListDal playListDal, ILibraryDal libraryDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _userRoleDal = userRoleDal;
            _favoriteDal = favoriteDal;
            _playListDal = playListDal;
            _libraryDal = libraryDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var roles = _userService.GetRoles(user);
            var accessToken = _tokenHelper.CreateToken(user, roles.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByMail(userLoginDto.UserEmail);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.Data.UserPasswordHash, userToCheck.Data.UserPasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IResult PasswordChange(string password, string oldPassword)
        {
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            
           if (!HashingHelper.VerifyPasswordHash(oldPassword, userDb.UserPasswordHash, userDb.UserPasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.OldPWNewPwSameNot);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                UserId = userDb.UserId,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                Status = userDb.Status,
                UserEmail = userDb.UserEmail,
                UserNickName = userDb.UserNickName,
                UserName = userDb.UserName,
                UserSurname = userDb.UserSurname
            };
            _userDal.Update(user);
            return new SuccessResult(Messages.UserPasswordIsChanged);
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                UserEmail = userRegisterDto.UserEmail,
                UserNickName = userRegisterDto.UserNickName,
                UserName = userRegisterDto.UserName,
                UserSurname = userRegisterDto.UserName,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                Status = true
            };
            _userDal.Add(user);
            var userRole = new UserRole()
            {
                RoleId = 2,
                UserId = user.UserId
            };
            _userRoleDal.Add(userRole);

            var favorite = new Favorite()
            {
                UserId = user.UserId

            };
            _favoriteDal.Add(favorite);

            var playList = new PlayList()
            {
                UserId = user.UserId,
                PL_DateTime = DateTime.Now,
                PL_Name = "Created",
                Status = true,
            };
            _playListDal.Add(playList);

            var library = new Library()
            {
                UserId = user.UserId,
                Status = true,
                Name = "Created",
                Type = "Created",
            };
            _libraryDal.Add(library);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public IResult UserExists(string email)
        {
            var user = _userService.GetByMail(email).Data;
            if (user != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
