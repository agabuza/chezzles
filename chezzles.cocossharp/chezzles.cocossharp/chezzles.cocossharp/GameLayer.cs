using System.Collections.Generic;
using CocosSharp;
using chezzles.cocossharp.Pieces;
using chezzles.engine.Data;
using System.Linq;
using chezzles.cocossharp.Extensions;
using chezzles.engine.Core.Game;

namespace chezzles.cocossharp
{
    public class GameLayer : CCLayerColor
    {
        private CocoPieceBuilder pieceBuilder;
        private CCTileMap tileMap;
        private float scaleFactor = 4;

        // change hardcoded values to get correct board place
        public static CCPoint Origin = new CCPoint(0, 0);
        //private CCButton pauseButton;
        //private CCButton nextButton;
        private GamesStorage storage;
        private int index;

        public GameLayer(CCSize size)
            : base(size)
        {
            this.pieceBuilder = new CocoPieceBuilder();
            this.storage = new GamesStorage();
        }

        private void InitializeMenu()
        {
            //var cache = CCSpriteFrameCache.SharedSpriteFrameCache;
            //cache.AddSpriteFrames("hd/buttons.plist");
            
            //this.pauseButton = new CCButton(cache["btn_pause.png"])
            //           .ScaleToSize(330, 150)
            //           .PlaceAt(.2f, .9f, 101, this);

            //this.pauseButton.Click += (s) =>
            //{
            //};

            //this.nextButton = new CCButton(cache["btn_next.png"])
            //    .ScaleToSize(330, 150)
            //    .PlaceAt(.8f, .9f, 101, this);

            //this.nextButton.Click += (s) =>
            //{
            //    var game = this.storage.Get(++this.index);
            //    if (game != null)
            //    {
            //        ClearBoard();
            //        DrawBoard(this, game);
            //    }
            //};

            //this.score = new CCLabel("Score 100% out of 12 puzzles", "arial", 36f)
            //    .PlaceAt(.2f, .1f, 101, this)
            //    .WithTextCentered();

        }

        private void ClearBoard()
        {
            var pieces = this.Children.OfType<CocoPiece>().ToList();
            foreach(var piece in pieces)
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

            var game = this.storage.Get(++this.index);
            DrawBoard(this, game);
        }

        private void DrawBoard(CCNode gameLayer, Game game)
        {
            game.Board.SetSize(this.tileMap.ContentSize.Width);
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
            //Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;
            AddBoard();
            this.InitializeMenu();
        }
    }
}