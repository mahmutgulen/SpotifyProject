using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFollowService
    {
        IResult FollowIT(int id);
        IResult UnFollow(int id);

        IDataResult<List<Follow>> GetMyTakipEttiklerim();
        IDataResult<List<Follow>> GetMyTakipEdenler();
    }
}
