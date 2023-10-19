using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.IdentityModel.Tokens.Jwt;

namespace Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private IFavoriteDal _favoriteDal;
        private IUserDal _userDal;
        private ISpotifyDal _spotifyDal;
        private ISongRepositoryDal _songRepositoryDal;
        private ILibraryDal _libraryDal;

        public FavoriteManager(IFavoriteDal favoriteDal, IUserDal userDal, ISpotifyDal spotifyDal, ISongRepositoryDal songRepositoryDal, ILibraryDal libraryDal)
        {
            _favoriteDal = favoriteDal;
            _userDal = userDal;
            _spotifyDal = spotifyDal;
            _songRepositoryDal = songRepositoryDal;
            _libraryDal = libraryDal;
        }

        public IResult AddToMyFavorites(int id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var favori = _favoriteDal.Get(x => x.SongId == id);
            var songs = _songRepositoryDal.Get(x => x.SongId == id);

            if (favori == null)
            {
                if (songs != null)
                {
                    var favorite = new Favorite
                    {
                        SongId = id,
                        UserId = userDb.UserId,
                        Status = true

                    };
                    _favoriteDal.Add(favorite);

                    //like sayısı ++
                    var songRepository = _spotifyDal.Get(x => x.SongId == favorite.SongId);
                    songRepository.SongLikesNumber = songRepository.SongLikesNumber + 1;
                    _spotifyDal.Update(songRepository);

                    return new SuccessResult(Messages.FavoritesAdded);
                }
                return new SuccessResult(Messages.IdNotExists);
            }
            return new SuccessResult(Messages.SongAlreadyExistsInFavorite);
        }

        public IResult DeleteOnMyFavorites(int songId)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //

            var favoris = _favoriteDal.Get(x => x.SongId == songId);
            if (favoris != null)
            {
                var favori = _favoriteDal.Get(x => x.SongId == songId);
                var favorites = new Favorite
                {
                    Status = false,
                    SongId = favori.SongId,
                    UserId = favori.UserId,
                    FavoritesId = favori.FavoritesId
                };
                _favoriteDal.Update(favorites);
                //like sayısı --
                var songRepository = _spotifyDal.Get(x => x.SongId == favori.SongId);
                songRepository.SongLikesNumber = songRepository.SongLikesNumber - 1;
                _spotifyDal.Update(songRepository);

                return new SuccessResult(Messages.SongIsDeleted);
            }
            return new ErrorResult(Messages.IdNotExists);
        }

        public IDataResult<List<Favorite>> GetMyFavorites()
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //Yukarıdaki user'ın favorileri listeleniyor
            var favorites = _favoriteDal.GetList(x => x.UserId == userDb.UserId).ToList();
            return new SuccessDataResult<List<Favorite>>(favorites);
        }
    }
}
