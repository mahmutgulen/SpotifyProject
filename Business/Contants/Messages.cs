using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contants
{
    public static class Messages
    {
        public static string UserDeleted = "Kullanıcı başarıyla silindi.";

        public static string UserAdded = "Kullanıcı başarıyla eklendi.";

        public static string UserUpdated = "Kullanıcı başarıyla güncellendi.";

        public static string UserNotFound = "Kullanıcı bulunamadı.";

        public static string UserAlreadyExists = "Email zaten kullanılıyor.";

        public static string UserPasswordIsChanged = "Kullanıcı şifresi başarıyla değiştirildi.";

        public static string AccessTokenCreated = "Token başarıyla oluşturuldu.";

        public static string PasswordError = "Şifre hatalı.";

        public static string SuccessfulLogin = "Giriş başarılı.";

        public static string UserRegistered = "Kayıt Başarılı.";

        public static string PasswordsCannotBeTheSame = "Yeni şifre eski şifreyle aynı olamaz.";

        public static string SongAddedToRepository = "Şarkı havuza başarıyla eklendi.";

        public static string FavoritesAdded = "Şarkı beğenilenler listesine eklendi.";

        public static string SongIsDeleted = "Şarkı başarıyla silindi.";

        public static string SongNotFoundInFavorites = "Favorilerinizde bu id'de şarkı bulunmuyor.";

        public static string PlayListAdded = "Playlist başarıyla oluşturuldu.";

        public static string SongAddedToPlaylist = "Şarkı başarıyla çalma listesine eklendi.";

        public static string FollowAdded = "Playlist takip edildi.";

        public static string FollowDeleted = "Bu playlist artık takip edilmiyor.";

        public static string PlayListDeleted = "Playlist başarıyla silindi.";

        public static string AlbumAdded = "Albüm başarıyla eklendi.";

        public static string SongDeletedInPlaylist = "Şarkı çalma listesinden çıkarıldı.";

        public static string SongNotFoundInPlaylist = "Şarkı playlist içerisinde bulunamadı.";

        public static string UserFollowed = "Kullanıcı takip edildi.";

        public static string UserUnFollowed = "Kullanıcı artık takip edilmiyor.";

        public static string SongAlreadyExistsInFavorite = "Şarkı zaten favorilerde mevcut.";

        public static string IdNotExists = "Şarkı bulunmadı.";

        public static string UserAlreadyFollow = "Kullanıcı zaten takip ediliyor.";

        public static string AlredayNotFollowing = "Kullanıcıyı zaten takip etmiyorsunuz.";

        public static string PlaylistNotFound = "Playlist bulunamadı.";

        public static string PlaylistAlreadyFollow = "Playlist zaten takip ediliyor.";

        public static string AlreadyNotFollowingPlaylist = "Bu çalma listesi bulunamadı.";

        public static string AlreadyExistsPlaylist = "Bu isimde zaten çalma listesi var, aynı isimde çalma listesi oluşturamazsın.";

        public static string SongAlreadyExistsInPlaylist = "Şarkı zaten çalma listesinde mevcut";

        public static string SongAlreadyDeleted = "Bu şarkı daha önce silindi.";

        public static string PlaylistAlreadyDelete = "Bu çalma listesi zaten silinmişti.";

        public static string AlbumExists = "Albüm zaten mevcut";

        public static string OldPWNewPwSameNot = "Yeni şifre eski şifre ile aynı olamaz.";
    }
}
