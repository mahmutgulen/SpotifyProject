using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPlayListService
    {
        IResult AddPlayList(string name);
        IResult AddSongInPlayList(int pl_id, int songId);
        IResult DeletePlaylist(int pl_id);
        IResult DeleteSongInPlayList(int pl_id, int songId);
    }
}
