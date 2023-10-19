using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.Library;
using System.IdentityModel.Tokens.Jwt;
using static Entities.Concrete.Dtos.Library.LibraryListDto;

namespace Business.Concrete
{
    public class LibraryManager : ILibraryService
    {
        private ILibraryDal _libraryDal;
        private IUserDal _userDal;
        private IFavoriteDal _favoriteDal;
        private IPlayListDal _playListDal;
        private IAlbumDal _albumDal;

        public LibraryManager(ILibraryDal libraryDal, IUserDal userDal, IFavoriteDal favoriteDal, IPlayListDal playListDal, IAlbumDal albumDal)
        {
            _libraryDal = libraryDal;
            _userDal = userDal;
            _favoriteDal = favoriteDal;
            _playListDal = playListDal;
            _albumDal = albumDal;
        }

        public IDataResult<List<LibraryListDto>> GetLibrary()
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //------
            var favorite = _favoriteDal.GetList(x => x.UserId == userDb.UserId);
            List<LibraryListDto.FavoriteListDto> favoritelistDto = new List<LibraryListDto.FavoriteListDto>();

            foreach (var item in favorite)
            {
                LibraryListDto.FavoriteListDto favoriteDto = new LibraryListDto.FavoriteListDto();
                favoriteDto.SongId = Convert.ToInt32(item.SongId);
                favoritelistDto.Add(favoriteDto);
            }
            //playlist
            var playlist = _playListDal.GetList(x => x.UserId == userDb.UserId);
            List<LibraryListDto.PL_ListDto> pl_listDto = new List<LibraryListDto.PL_ListDto>();

            foreach (var item in playlist)
            {
                LibraryListDto.PL_ListDto pl_Dto = new PL_ListDto();
                pl_Dto.PlaylistName = item.PL_Name;
                pl_listDto.Add(pl_Dto);
            }

            //albums
            var album = _albumDal.GetList(x => x.id == userDb.UserId);

            List<LibraryListDto.AlbumlistDto> albumListDto = new List<LibraryListDto.AlbumlistDto>();

            foreach (var item in album)
            {
                LibraryListDto.AlbumlistDto album_dto = new AlbumlistDto();
                album_dto.AlbumName = item.AlbumName;
                album_dto.AlbumArtist = item.AlbumArtist;
                albumListDto.Add(album_dto);
            }
            //mapping
            var libraryDATA = _libraryDal.GetList(x => x.UserId == userDb.UserId);
            List<LibraryListDto> list = new List<LibraryListDto>();
            //foreach (var items in libraryDATA)
            // {
            LibraryListDto libraryDto = new LibraryListDto()
            {
                Favorite = favoritelistDto,
                Playlist = pl_listDto,
                Album = albumListDto

            };
            list.Add(libraryDto);
            // }
            return new SuccessDataResult<List<LibraryListDto>>(list);
        }
    }
}
