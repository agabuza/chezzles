using chezzles.engine.Core.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(chezzles.cocossharp.Services.GameService))]
namespace chezzles.cocossharp.Services
{
    public class GameService : IRestService<Game>
    {
        private HttpClient client;
        private bool disposed;

        public GameService()
        {
            this.client = new HttpClient();
            this.client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Game>> GetAll()
        {
            var serviceUrl = @"http://chezzles.azurewebsites.net/api/puzzles";
            var uri = new Uri(serviceUrl);
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strGames = JsonConvert.DeserializeObject<List<GameDTO>>(content);
                var parser = new GameParser();
                return parser.Parse(string.Join(" ", strGames.Select(x => x.PgnString))).ToList();
            }

            return null;
        }

        public async Task<Game> GetById(int id)
        {
            var serviceUrl = @"http://chezzles.azurewebsites.net/api/puzzles/{0}";
            var uri = new Uri(string.Format(serviceUrl, id));
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strGame = JsonConvert.DeserializeObject<GameDTO>(content);
                var parser = new GameParser();
                return parser.Parse(strGame.PgnString).FirstOrDefault();
            }

            return null;
        }

        public async Task<Game> Next()
        {
            // Find out a way to get next item for current user;
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                client.Dispose();
            }

            disposed = true;
        }

        ~GameService()
        {
            Dispose(false);
        }
    }
}
