using System.Collections.Generic;
using CocosSharp;
using chezzles.cocossharp.Pieces;
using chezzles.engine.Data;
using System.Linq;
using chezzles.cocossharp.Extensions;
using chezzles.engine.Core.Game;
using GalaSoft.MvvmLight.Messaging;
using chezzles.cocossharp.Messages;
using chezzles.engine.Core.Game.Messages;
using Xamarin.Forms;
using chezzles.cocossharp.Services;

namespace chezzles.cocossharp
{
    public class GameLayer : CCLayerColor
    {
        private CocoPieceBuilder pieceBuilder;
        private CCTileMap tileMap;
        private float scaleFactor = 4;
        private IMessenger messenger = Messenger.Default;

        // change hardcoded values to get correct board place
        public static CCPoint Origin = new CCPoint(0, 0);
        private int index = 1;
        private IRestService<Game> service;

        public GameLayer(CCSize size)
            : base(size)
        {
            this.pieceBuilder = new CocoPieceBuilder();
            this.service = DependencyService.Get<IRestService<Game>>();
            Color = CCColor3B.DarkGray;

            this.messenger.Register<NextPuzzleMessage>(this, (msg) =>
            {
                var game = this.service.GetById(this.index++).Result;
                ClearBoard();
                DrawBoard(this, game);
                this.messenger.Send(new PuzzleLoadedMessage(game.Board.IsWhiteMove, string.Empty));
            });

            this.messenger.Register<SkipPuzzleMessage>(this, (msg) =>
            {
                var game = this.service.GetById(this.index++).Result;
                ClearBoard();
                DrawBoard(this, game);
                this.messenger.Send(new PuzzleLoadedMessage(game.Board.IsWhiteMove, string.Empty));
            });
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
    }
}