using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFavoriteService
    {
        IDataResult<List<Favorite>> GetMyFavorites();

        IResult AddToMyFavorites(int id);
        IResult DeleteOnMyFavorites(int songId);
    }
}
