using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserRole>().As<UserRole>();
            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();

            builder.RegisterType<FavoriteManager>().As<IFavoriteService>();
            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>();

            builder.RegisterType<LibraryManager>().As<ILibraryService>();
            builder.RegisterType<EfLibraryDal>().As<ILibraryDal>();

            builder.RegisterType<PlayListManager>().As<IPlayListService>();
            builder.RegisterType<EfPlayListDal>().As<IPlayListDal>();

            builder.RegisterType<SpotifyManager>().As<ISpotifyService>();//
            builder.RegisterType<EfSpotifyDal>().As<ISpotifyDal>();

            builder.RegisterType<EfPlayListItemDal>().As<IPlayListItemDal>();
            builder.RegisterType<EfAlbumDal>().As<IAlbumDal>();

            builder.RegisterType<EfSongRepositoryDal>().As<ISongRepositoryDal>();

            builder.RegisterType<PlayListsIFollowManager>().As<IPlayListsIFollowService>();
            builder.RegisterType<EfPlayListsIFollowDal>().As<IPlayListsIFollowDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<FollowManager>().As<IFollowService>();
            builder.RegisterType<EfFollowDal>().As<IFollowDal>();

        }
    }
}
