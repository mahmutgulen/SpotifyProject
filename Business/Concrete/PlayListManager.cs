using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Dtos.PlayList;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlayListManager : IPlayListService
    {
        private IPlayListDal _playListDal;
        private IUserDal _userDal;
        private IPlayListItemDal _playListItemDal;
        private ILibraryDal _libraryDal;
        private ISongRepositoryDal _songRepositoryDal;
        public PlayListManager(IPlayListDal playListDal, IUserDal userDal, IPlayListItemDal playListItemDal, ILibraryDal libraryDal, ISongRepositoryDal songRepositoryDal)
        {
            _playListDal = playListDal;
            _userDal = userDal;
            _playListItemDal = playListItemDal;
            _libraryDal = libraryDal;
            _songRepositoryDal = songRepositoryDal;
        }

        public IResult AddPlayList(string name)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            var playlist = _playListDal.Get(x => x.PL_Name == name);
            if (playlist == null)
            {
                var playList = new PlayList
                {
                    PL_Name = name,
                    PL_SongCount = 0,
                    UserId = userDb.UserId,
                    PL_DateTime = DateTime.Now,
                    Status = true
                };
                _playListDal.Add(playList);

                var library = new Library
                {
                    Type = "Playlist",
                    Name = playList.PL_Name,
                    PL_Id = playList.PL_Id,
                    UserId = userDb.UserId,
                    Status = true
                };
                _libraryDal.Add(library);

                return new SuccessResult(Messages.PlayListAdded);
            }
            return new ErrorResult(Messages.AlreadyExistsPlaylist);
        }

        public IResult AddSongInPlayList(int pl_id, int songId)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            
            var playlistData = _playListDal.Get(x => x.PL_Id == pl_id);
            var plItemsData = _playListItemDal.Get(x => x.SongId == songId);
            var songData = _songRepositoryDal.Get(x => x.SongId == songId);
            if (songData != null)
            {
                if (plItemsData == null)
                {
                    var playlistItem = new PlayListItem
                    {
                        PL_Id = pl_id,
                        SongId = songId,
                        UserId = userDb.UserId,
                        Status = true
                    };
                    _playListItemDal.Add(playlistItem);




                    //şarkı sayısı ++
                    var playlist = _playListDal.Get(x => x.PL_Id == playlistItem.PL_Id);
                    playlist.PL_SongCount = playlist.PL_SongCount + 1;
                    _playListDal.Update(playlist);

                    return new SuccessResult(Messages.SongAddedToPlaylist);
                }
                return new ErrorResult(Messages.SongAlreadyExistsInPlaylist);
            }
            return new ErrorResult(Messages.IdNotExists);


        }

        public IResult DeletePlaylist(int pl_id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //
            //playlis silme
            var plDATA = _playListItemDal.Get(x => x.PL_Id == pl_id);
            if (plDATA != null)
            {
                if (plDATA.Status != false)
                {
                    var playlist = _playListDal.Get(x => x.PL_Id == pl_id);
                    var playlists = new PlayList
                    {
                        PL_Id = playlist.PL_Id,
                        PL_DateTime = playlist.PL_DateTime,
                        PL_FollowCount = playlist.PL_FollowCount,
                        PL_Name = playlist.PL_Name,
                        UserId = playlist.UserId,
                        PL_SongCount = playlist.PL_SongCount,
                        Status = false
                    };
                    _playListDal.Update(playlists);

                    var playlistItem = _playListItemDal.Get(x => x.PL_Id == pl_id);
                    var playlistsItems = new PlayListItem
                    {
                        PL_Id = playlistItem.PL_Id,
                        SongId = playlistItem.SongId,
                        UserId = playlistItem.UserId,
                        id = playlistItem.PL_Id,
                        Status = false
                    };
                    _playListItemDal.Update(playlistsItems);

                    var libraryData = _libraryDal.Get(x=> x.PL_Id == pl_id);

                    var library = new Library
                    {
                        id = libraryData.id,
                        Name = libraryData.Name,
                        PL_Id= libraryData.PL_Id,
                        Type = libraryData.Type,
                        UserId= libraryData.UserId,
                        Status=false
                    };
                    _libraryDal.Update(library);
                    return new SuccessResult(Messages.PlayListDeleted);
                }
                return new ErrorResult(Messages.PlaylistAlreadyDelete);
            }
            return new ErrorResult(Messages.PlaylistNotFound);
        }

        public IResult DeleteSongInPlayList(int pl_id, int songId)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var plDATA = _playListItemDal.Get(x => x.SongId == songId);

            if (plDATA != null)
            {
                if (plDATA.Status != false)
                {
                    var playlistItem = _playListItemDal.Get(x => x.SongId == songId);
                    var playlistsItems = new PlayListItem
                    {
                        PL_Id = playlistItem.PL_Id,
                        SongId = playlistItem.SongId,
                        UserId = playlistItem.UserId,
                        id = playlistItem.id,
                        Status = false

                    };
                    _playListItemDal.Update(playlistsItems);

                    return new SuccessResult(Messages.SongDeletedInPlaylist);
                }
                return new ErrorResult(Messages.SongAlreadyDeleted);
            }
            return new ErrorResult(Messages.SongNotFoundInPlaylist);


        }
    }
}
