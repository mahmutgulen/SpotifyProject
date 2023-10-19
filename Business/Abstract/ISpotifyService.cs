using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISpotifyService
    {
        Task<IDataResult<Song>> GetByIdInSpotify(string id);
        IDataResult<SpotifyToken> GetSpotifyToken();
        IResult AddToSongRepository(string id);
        IResult AddToAlbum(string id);
        IDataResult<List<SongRepository>> GetSongRepository();


    }
}
