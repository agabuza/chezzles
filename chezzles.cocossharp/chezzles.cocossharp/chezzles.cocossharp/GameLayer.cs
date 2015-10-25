using System;
using System.Collections.Generic;
using CocosSharp;
using chezzles.cocossharp.Pieces;
using chezzles.engine.Pieces;
using chezzles.engine.Core;
using chezzles.engine.Data;
using System.Linq;

namespace chezzles.cocossharp
{
    public class GameLayer : CCLayerColor
    {
        private CocoPieceBuilder pieceBuilder;
        private Board board;
        private CCTileMap tileMap;
        private float scaleFactor = 4;
        public static CCPoint Origin = new CCPoint(0, 0);

        public GameLayer(CCSize size)
            : base(size)
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;

            AddEventListener(touchListener, this);
            this.pieceBuilder = new CocoPieceBuilder();
            Color = CCColor3B.Green;
        }

        private void AddBoard()
        {
            this.tileMap = new CCTileMap("tilemaps/board.tmx");
            this.scaleFactor = Layer.VisibleBoundsWorldspace.Size.Width / tileMap.TileLayersContainer.ContentSize.Width;
            this.tileMap.Scale = scaleFactor;

            tileMap.PositionX = Origin.X;
            tileMap.PositionY = Origin.Y;
            tileMap.AnchorPoint = new CCPoint(0, 0);

            tileMap.Antialiased = false;
            this.AddChild(tileMap, -1);
        }

        private void AddPieces()
        {
            this.DrawBoard(this);
            //var bishop = this.pieceBuilder.Build(new Bishop(new Square(2, 2), board, PieceColor.Black));
            //bishop.Scale = this.scaleFactor - 0.5f;
            //this.AddChild(bishop, 99);

            //var rook = this.pieceBuilder.Build(new Rook(new Square(1, 1), board, PieceColor.White));
            //rook.Scale = this.scaleFactor - 0.5f;
            //this.AddChild(rook, 99);

            //var queen = this.pieceBuilder.Build(new Queen(new Square(7, 2), board, PieceColor.Black));
            //queen.Scale = this.scaleFactor - 0.5f;
            //this.AddChild(queen, 99);

            //var knight = this.pieceBuilder.Build(new Knight(new Square(7, 4), board, PieceColor.White));
            //knight.Scale = this.scaleFactor - 0.5f;
            //this.AddChild(knight, 99);
        }

        private void DrawBoard(CCNode gameLayer)
        {
            var storage = new GamesStorage();
            var game = storage.GetGames().FirstOrDefault();
            game.Board.SetSize(Window.WindowSizeInPixels.Width);
            foreach (var piece in game.Board.Pieces)
            {
                var cocoPiece = this.pieceBuilder.Build(piece);
                cocoPiece.Scale = this.scaleFactor - 0.5f;
                gameLayer.AddChild(cocoPiece, 99);
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;
            AddBoard();
            AddPieces();
        }

        private void OnTouchesEnded(List<CCTouch> arg1, CCEvent arg2)
        {
        }

        private void ScaleToSize(CCNode sprite, float width, float height)
        {
            sprite.ScaleX = width / sprite.ContentSize.Width;
            sprite.ScaleY = height / sprite.ContentSize.Height;
        }
    }
}