using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using chezzles.core.api.Dto;
using CoreGame = chezzles.core.engine.Core.Game;
using Newtonsoft.Json;

namespace chezzles.core.api.Services
{
    public class GameService : IPuzzleService<CoreGame.Game>
    {
        private HttpClient client;
        private bool disposed;

        public GameService()
        {
            this.client = new HttpClient();
            this.client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<CoreGame.Game>> GetAll()
        {
            var serviceUrl = @"http://chezzles.azurewebsites.net/api/puzzles";
            var uri = new Uri(serviceUrl);
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strGames = JsonConvert.DeserializeObject<List<GameDto>>(content);
                var parser = new engine.Core.Game.GameParser();
                return parser.Parse(string.Join(" ", strGames.Select(x => x.PgnString))).ToList();
            }

            return null;
        }

        public async Task<CoreGame.Game> GetById(int id)
        {
            var serviceUrl = @"http://chezzles.azurewebsites.net/api/puzzles/{0}";
            var uri = new Uri(string.Format(serviceUrl, id));
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var strGame = JsonConvert.DeserializeObject<GameDto>(content);
                var parser = new engine.Core.Game.GameParser();
                return parser.Parse(strGame.PgnString).FirstOrDefault();
            }

            return null;
        }

        public async Task<CoreGame.Game> Next()
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
