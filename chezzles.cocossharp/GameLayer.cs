using CocosSharp;
using chezzles.cocossharp.Pieces;
using System.Linq;
using chezzles.engine.Core.Game;
using GalaSoft.MvvmLight.Messaging;
using chezzles.cocossharp.Messages;
using chezzles.engine.Core.Game.Messages;
using Xamarin.Forms;
using chezzles.cocossharp.Services;
using System.Threading.Tasks;
using chezzles.cocossharp.Common;
using Newtonsoft.Json;
using chezzles.cocossharp.Pieces.Model;

namespace chezzles.cocossharp
{
    public class GameLayer : CCLayerColor
    {
        private const string PUZZLE_ID = "puzzleId";
        private CocoPieceBuilder pieceBuilder;
        private CCTileMap tileMap;
        private float scaleFactor = 4;

        public static CCPoint Origin = new CCPoint(0, 0);

        private IRestService<Game> service;
        private ISetttingsProvider settings;
        private IMessenger messenger;

        public GameLayer(CCSize size)
            : base(size)
        {
            PuzzleId = int.Parse(string.IsNullOrEmpty(this.Settings[PUZZLE_ID]) ? "0" : this.Settings[PUZZLE_ID]);
            this.pieceBuilder = new CocoPieceBuilder();
            Color = CCColor3B.DarkGray;

            this.Messenger.Register<NextPuzzleMessage>(this, LoadNextPuzzle);
            this.Messenger.Register<SkipPuzzleMessage>(this, LoadNextPuzzle);
        }

        private void LoadNextPuzzle(NextPuzzleMessage obj)
        {
            Task.Run(async () =>
            {
                var game = await this.Service.GetById(++this.PuzzleId);
                Device.BeginInvokeOnMainThread(() =>
                {
                    ClearBoard();
                    DrawBoard(this, game);
                    this.Messenger.Send(new PuzzleLoadedMessage(game.Board.IsWhiteMove, string.Empty));
                });
            });
        }

        public int PuzzleId { get; set; }

        public ISetttingsProvider Settings
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = DependencyService.Get<ISetttingsProvider>(DependencyFetchTarget.GlobalInstance);
                }

                return this.settings;
            }

            internal set
            {
                this.settings = value;
            }
        }

        public IRestService<Game> Service
        {
            get
            {
                if (this.service == null)
                {
                    this.service = DependencyService.Get<IRestService<Game>>();
                }

                return this.service;
            }

            internal set
            {
                this.service = value;
            }
        }

        public IMessenger Messenger
        {
            get
            {
                if (this.messenger == null)
                {
                    this.messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default;
                }

                return messenger;
            }

            internal set
            {
                messenger = value;
            }
        }

        private void ClearBoard()
        {
            var pieces = this.Children.OfType<CocoPiece>().ToList();
            foreach (var piece in pieces)
            {
                this.RemoveChild(piece);
            }
        }

        private void AddBoard()
        {
            var boardPath = "tilemaps/board.tmx";
            var set = settings["chess-set"];
            var chessSet = JsonConvert.DeserializeObject<ChessSet>(set);
            if (chessSet != null && !string.IsNullOrEmpty(chessSet.BoardPath))
            {
                boardPath = chessSet.BoardPath;
            }

            this.tileMap = new CCTileMap(boardPath);
            this.scaleFactor = this.ContentSize.Width / tileMap.TileLayersContainer.ContentSize.Width;
            this.tileMap.Scale = scaleFactor;

            tileMap.PositionX = Origin.X;
            tileMap.PositionY = Origin.Y;
            tileMap.AnchorPoint = new CCPoint(0, 0);
            tileMap.Antialiased = false;
            this.AddChild(tileMap, -1);
        }

        private void DrawBoard(CCNode gameLayer, Game game)
        {
            game.Board.SetSize(this.tileMap.ContentSize.Width);
            foreach (var piece in game.Board.Pieces)
            {
                var cocoPiece = this.pieceBuilder.Build(piece);
                gameLayer.AddChild(cocoPiece, 99);
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            AddBoard();
        }

        internal void Save()
        {
            this.Settings[PUZZLE_ID] = PuzzleId.ToString();
            this.Settings.SaveAsync();
        }

        protected override void Dispose(bool disposing)
        {
            this.Save();
            if (this.service != null)
            {
                this.service.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}