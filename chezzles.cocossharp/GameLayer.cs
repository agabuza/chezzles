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
using System;
using chezzles.cocossharp.Common;

namespace chezzles.cocossharp
{
    public class GameLayer : CCLayerColor
    {
        private const string PUZZLE_ID = "puzzleId";
        private CocoPieceBuilder pieceBuilder;
        private CCTileMap tileMap;
        private float scaleFactor = 4;
        private IMessenger messenger = Messenger.Default;

        // change hardcoded values to get correct board place
        public static CCPoint Origin = new CCPoint(0, 0);
        private int index = 1;
        private IRestService<Game> service;
        private ISetttingsProvider settings;

        public GameLayer(CCSize size)
            : base(size)
        {
            this.settings = DependencyService.Get<ISetttingsProvider>(DependencyFetchTarget.GlobalInstance);
            this.service = DependencyService.Get<IRestService<Game>>();
            PuzzleId = int.Parse(string.IsNullOrEmpty(this.settings[PUZZLE_ID]) ? "0" : this.settings[PUZZLE_ID]);
            this.pieceBuilder = new CocoPieceBuilder();
            Color = CCColor3B.DarkGray;

            this.messenger.Register<NextPuzzleMessage>(this, LoadNextPuzzle);
            this.messenger.Register<SkipPuzzleMessage>(this, LoadNextPuzzle);
        }

        private void LoadNextPuzzle(NextPuzzleMessage obj)
        {
            Task.Run(async () =>
            {
                var game = await this.service.GetById(++this.PuzzleId);
                Device.BeginInvokeOnMainThread(() =>
                {
                    ClearBoard();
                    DrawBoard(this, game);
                    this.messenger.Send(new PuzzleLoadedMessage(game.Board.IsWhiteMove, string.Empty));
                });
            });
        }

        public int PuzzleId { get; set; }

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
            this.tileMap = new CCTileMap("tilemaps/board.tmx");
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
            this.settings[PUZZLE_ID] = PuzzleId.ToString();
            this.settings.SaveAsync();
        }
    }
}