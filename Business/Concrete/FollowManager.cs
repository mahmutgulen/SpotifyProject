using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FollowManager : IFollowService
    {
        private IFollowDal _followDal;
        private IUserDal _userDal;

        public FollowManager(IFollowDal followDal, IUserDal userDal)
        {
            _followDal = followDal;
            _userDal = userDal;
        }

        public IResult FollowIT(int id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var userData = _userDal.Get(x => x.UserId == id);
            if (userData != null)
            {
                var followData = _followDal.Get(x => x.FollowedId == id);
                if (followData == null)
                {
                    //takip ediyor
                    var follow = new Follow
                    {
                        FollowerId = userDb.UserId,
                        FollowedId = id,
                        Status = true
                    };
                    _followDal.Add(follow);

                    //takip sayısı ++
                    var users = _userDal.Get(x => x.UserId == id);
                    users.Followers += 1;
                    _userDal.Update(users);
                    return new SuccessResult(Messages.UserFollowed);
                }
                return new ErrorResult(Messages.UserAlreadyFollow);
            }
            return new ErrorResult(Messages.UserNotFound);

        }

        public IDataResult<List<Follow>> GetMyTakipEdenler()
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            var follows = _followDal.GetList(x => x.FollowedId == userDb.UserId).ToList();

            return new SuccessDataResult<List<Follow>>(follows);

        }

        public IDataResult<List<Follow>> GetMyTakipEttiklerim()
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var follows = _followDal.GetList(x => x.FollowerId == userDb.UserId).ToList();
            return new SuccessDataResult<List<Follow>>(follows);
        }

        public IResult UnFollow(int id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var followData = _followDal.Get(x => x.FollowedId == id);
            if (followData != null)
            {
                var follow = _followDal.Get(x => x.FollowedId == id);
                var follows = new Follow
                {
                    id = follow.id,
                    FollowerId = follow.FollowerId,
                    FollowedId = follow.FollowedId,
                    Status = false
                };
                _followDal.Update(follows);

                //takip sayısı --
                var users = _userDal.Get(x => x.UserId == id);
                users.Followers = users.Followers - 1;
                _userDal.Update(users);
                //user folow eksi değere düşmesin diye 
                if (users.Followers <= 0)
                {
                    var FollowFix = new User
                    {
                        Followers = 0,
                        Status = users.Status,
                        UserEmail = users.UserEmail,
                        UserId = users.UserId,
                        UserName = users.UserName,
                        UserNickName = users.UserNickName,
                        UserSurname = users.UserSurname,
                        UserPasswordHash = users.UserPasswordHash,
                        UserPasswordSalt = users.UserPasswordSalt
                    };
                    _userDal.Update(FollowFix);
                }
                return new SuccessResult(Messages.UserUnFollowed);
            }
            return new ErrorResult(Messages.AlredayNotFollowing);

        }
    }
}
