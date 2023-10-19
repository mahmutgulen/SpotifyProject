using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete.Dtos.Library
{
    public class LibraryListDto : IDto
    {
        public List<FavoriteListDto> Favorite { get; set; }
        public List<PL_ListDto> Playlist { get; set; }
        public List<AlbumlistDto> Album { get; set; }

        public class FavoriteListDto : IDto
        {
            public int SongId { get; set; }
        }

        public class PL_ListDto
        {
            public string PlaylistName{ get; set; }
        }

         public class AlbumlistDto
        {
            public string AlbumName { get; set; }
            public string AlbumArtist { get; set; }
        }
    }
}
