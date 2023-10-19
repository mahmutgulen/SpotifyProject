using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlayListsIFollowManager : IPlayListsIFollowService
    {
        private IUserDal _userDal;
        private IPlayListsIFollowDal _playListsIFollowDal;
        private IPlayListDal _playListDal;

        public PlayListsIFollowManager(IUserDal userDal, IPlayListsIFollowDal playListsIFollowDal, IPlayListDal playListDal)
        {
            _userDal = userDal;
            _playListsIFollowDal = playListsIFollowDal;
            _playListDal = playListDal;
        }


        public IResult AddFollowPlaylist(int Pl_Id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            var playlistData = _playListDal.Get(x => x.PL_Id == Pl_Id);
            var playlistfollowcontrol = _playListsIFollowDal.Get(x => x.PL_Id == Pl_Id);
            if (playlistData != null)
            {
                if (playlistfollowcontrol == null)
                {
                    var plFollow = new PlayListsIFollow
                    {
                        UserId = userDb.UserId,
                        PL_Id = Pl_Id,
                        Status = true
                    };
                    _playListsIFollowDal.Add(plFollow);

                    //takip sayısı ++
                    var playlist = _playListDal.Get(x => x.PL_Id == plFollow.PL_Id);
                    playlist.PL_FollowCount = playlist.PL_FollowCount + 1;
                    _playListDal.Update(playlist);
                    return new SuccessResult(Messages.FollowAdded);
                }
                return new ErrorResult(Messages.PlaylistAlreadyFollow);

            }
            return new ErrorResult(Messages.PlaylistNotFound);

        }

        public IResult DeleteFollowPlaylist(int Pl_Id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            var playlistData = _playListDal.Get(x => x.PL_Id == Pl_Id);
            var playlistfollowcontrol = _playListsIFollowDal.Get(x => x.PL_Id == Pl_Id);
            if (playlistData != null)
            {
                var follow = _playListsIFollowDal.Get(x => x.PL_Id == Pl_Id);
                var follows = new PlayListsIFollow
                {
                    PL_Id = follow.PL_Id,
                    id = follow.id,
                    UserId = follow.UserId,
                    Status = false
                };
                _playListsIFollowDal.Update(follows);

                //takip sayısı --
                var playlist = _playListDal.Get(x => x.PL_Id == follow.PL_Id);
                playlist.PL_FollowCount = playlist.PL_FollowCount - 1;
                _playListDal.Update(playlist);

                return new SuccessResult(Messages.FollowDeleted);
            }
            return new ErrorResult(Messages.AlreadyNotFollowingPlaylist);
        }

        public IDataResult<List<PlayListsIFollow>> GetMyFollows()
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //Yukarıdaki user'ın takip ettiği çalma listeleri listeleniyor

            var follows = _playListsIFollowDal.GetList(x => x.UserId == userDb.UserId).ToList();

            return new SuccessDataResult<List<PlayListsIFollow>>(follows);
        }
    }
}
