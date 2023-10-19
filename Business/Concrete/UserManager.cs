using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserRoleDal _userRoleDal;

        public UserManager(IUserDal userDal, IUserRoleDal userRoleDal)
        {
            _userDal = userDal;
            _userRoleDal = userRoleDal;
        }

        public IResult Add(UserAddDto userAddDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                UserEmail = userAddDto.UserEmail,
                UserNickName = userAddDto.UserNickName,
                UserName = userAddDto.UserName,
                UserSurname = userAddDto.UserSurname,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                Status = true
            };

            var userRole = new UserRole()
            {
                UserId=user.UserId,
                RoleId=2
            };

            _userDal.Add(user);
            _userRoleDal.Add(userRole);

            return new SuccessResult(Messages.UserAdded);

        }

        public IResult Delete(int userId)
        {
            var user = _userDal.Get(x => x.UserId == userId);
            if (user != null)
            {
                _userDal.Delete(user);
                return new SuccessResult(Messages.UserDeleted);
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<User> GetById(int userId)
        {
            var user = _userDal.Get(x => x.UserId == userId);
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(x => x.UserEmail == email);
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        }

        public IDataResult<List<Role>> GetRoles(User user)
        {
            return new SuccessDataResult<List<Role>>(_userDal.GetRoles(user));
        }

        public IResult Update(UserUpdateDto userUpdateDto,string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                UserId = userUpdateDto.UserId,
                UserName = userUpdateDto.UserName,
                UserSurname = userUpdateDto.UserName,
                UserEmail = userUpdateDto.UserName, 
                UserNickName = userUpdateDto.UserName,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                Status=true
             };

            var role = new UserRole()
            {
                RoleId=userUpdateDto.UserRoleId
            };

            _userDal.Update(user);
            _userRoleDal.Update(role);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
