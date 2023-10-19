using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SpotifyManager : ISpotifyService
    {
        private ISpotifyDal _spotifyDal;
        private IAlbumDal _albumDal;
        private ILibraryDal _libraryDal;
        private IUserDal _userDal;
        private ISongRepositoryDal _songRepositoryDal;

        public SpotifyManager(ISpotifyDal spotifyDal, IAlbumDal albumDal, ILibraryDal libraryDal, IUserDal userDal, ISongRepositoryDal songRepositoryDal)
        {
            _spotifyDal = spotifyDal;
            _albumDal = albumDal;
            _libraryDal = libraryDal;
            _userDal = userDal;
            _songRepositoryDal = songRepositoryDal;
        }

        public IResult AddToAlbum(string id)
        {
            //token üzerinden id alıyorum, işlem yapabilmek için.
            var stream = MyToken.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var userDb = _userDal.Get(x => x.UserId == userId);
            //istek

            HttpClient client = new HttpClient();
            string spotifyToken = MyToken.spotifyToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", spotifyToken);
            var response = client.GetAsync($"https://api.spotify.com/v1/albums/{id}").Result;
            var responseBody = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<AlbumApi>(responseBody.Result);
            //albume ekliyor

            var albumDATA = _albumDal.Get(x => x.AlbumId == id);
            if (albumDATA == null)
            {
                var albums = new Album
                {
                    AlbumId = id,
                    AlbumArtist = data.artists[0].name.ToString(),
                    AlbumName = data.name,
                    Status = true
                };
                _albumDal.Add(albums);
                //kütüphaneye ekliyor
                var library = new Library
                {
                    Type = "Album",
                    Name = albums.AlbumName,
                    PL_Id = 0,
                    UserId = userDb.UserId,
                    Status = true

                };
                _libraryDal.Add(library);

                return new SuccessResult(Messages.AlbumAdded);
            }
            return new ErrorResult(Messages.AlbumExists);
            
        }

        public IResult AddToSongRepository(string id)
        {
            HttpClient client = new HttpClient();
            string spotifyToken = MyToken.spotifyToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", spotifyToken);
            var response = client.GetAsync($"https://api.spotify.com/v1/tracks/{id}").Result;
            var responseBody = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Song>(responseBody.Result);

            //  if gelen veri null kontorlü, liste için

            var songRepository = new SongRepository
            {
                //SongArtist = data.artists[0].name.ToString() + " & " + data.artists[1].name.ToString(),
                SongArtist = data.artists[0].name.ToString(),
                SongName = data.name,
                Status = true
            };
            _spotifyDal.Add(songRepository);
            return new SuccessResult(Messages.SongAddedToRepository);
        }

        public async Task<IDataResult<Song>> GetByIdInSpotify(string id)
        {

            HttpClient client = new HttpClient();
            string spotifyToken = MyToken.spotifyToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", spotifyToken);
            var response = client.GetAsync($"https://api.spotify.com/v1/tracks/{id}").Result;
            var responseBody = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Song>(responseBody.Result);

            return new SuccessDataResult<Song>(data);
        }

        public IDataResult<List<SongRepository>> GetSongRepository()
        {
            var songData = _songRepositoryDal.GetList().ToList();
            return new SuccessDataResult<List<SongRepository>>(songData);
        }

        public IDataResult<SpotifyToken> GetSpotifyToken()
        {
            var spotifyClient = "4931c86306244407910fcdde1da23831";
            var spotifySecret = "4b9eeddb0f9b459c857abbdcd7428132";
            var webClient = new WebClient();
            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "client_credentials");
            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{spotifyClient}:{spotifySecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);
            var tokenResponse = webClient.UploadValues("https://accounts.spotify.com/api/token/", postparams);
            var textResponse = Encoding.UTF8.GetString(tokenResponse);
            var token = JsonConvert.DeserializeObject<SpotifyToken>(textResponse);

            return new SuccessDataResult<SpotifyToken>(token.access_token);
        }



    }
}
