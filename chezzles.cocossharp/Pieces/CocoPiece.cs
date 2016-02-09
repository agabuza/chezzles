using chezzles.cocossharp.Extensions;
using chezzles.engine.Core;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chezzles.cocossharp.Pieces
{
    public class CocoPiece : CCSprite
    {
        private Piece piece;
        private CCEventListenerTouchOneByOne touchListener;
        private CCPoint initPosition;
        private CCDrawNode drawNode;
        private Square lastMovePosition;
        private CCDrawNode possibleMoves;
        private List<Square> possibleSquares = new List<Square>();

        public float radius { get { return ContentSize.Width * 0.5f; } }

        public PieceType PieceType
        {
            get { return piece.Type; }
        }

        public CocoPiece(Piece piece, CCSpriteFrame cCSpriteFrame) 
            : base(cCSpriteFrame)
        {
            this.ScaleX = 118 / cCSpriteFrame.Texture.ContentSizeInPixels.Width + 0.1f;
            this.ScaleY = 118 / cCSpriteFrame.Texture.ContentSizeInPixels.Width + 0.1f;
            this.piece = piece;
            this.piece.PieceTaken += OnPieceTaken;
            this.piece.PositionUpdated += (s, p) =>
            {
                this.Position = p.GetPoint(this.piece.Board.Size);
            };

            AnchorPoint = CCPoint.AnchorMiddle;
            Position = this.GetPosition();
            touchListener = new CCEventListenerTouchOneByOne();
            
            touchListener.OnTouchMoved = Touch;
            touchListener.OnTouchBegan = ToucheBegan;
            touchListener.OnTouchEnded = TouchEnded;
            touchListener.OnTouchCancelled = TouchCanceled;
            AddEventListener(touchListener, this);
        }

        private void OnPieceTaken(object sender)
        {
            this.RemoveFromParent();
        }

        private bool ToucheBegan(CCTouch touch, CCEvent e)
        {
            this.initPosition = this.Position;
            var scaledWidth = ContentSize.Width * ScaleX;
            var scaledHeight = ContentSize.Height * ScaleY;

            var rect = new CCRect(
                PositionWorldspace.X - scaledWidth / 2, 
                PositionWorldspace.Y - scaledHeight / 2, 
                scaledWidth, 
                scaledHeight);            

            if (!rect.ContainsPoint(touch.Location))
            {
                return false;
            }

            this.AnchorPoint = new CCPoint(0.2f, 0.5f);
            this.Scale = this.ScaleX * 3f;
            this.possibleSquares = this.piece.PossibleMoves().ToList();
            this.DrawPossibleMoves();
            return true;
        }

        private void DrawPossibleMoves()
        {
            var size = this.piece.Board.Size / 8;
            foreach (var point in this.possibleSquares.Select(x => x.GetPoint(this.piece.Board.Size)))
            {
                //this.possibleMoves.DrawSolidCircle(point, 20, new CCColor4B(225, 137, 96, 0x66));
                this.possibleMoves.DrawRect(point, size, new CCColor4B(225, 137, 96, 0x66));
            }
        }

        private void TouchCanceled(CCTouch touch, CCEvent e)
        {
            this.Position = this.initPosition;
            drawNode.Clear();
            possibleMoves.Clear();
            possibleSquares.Clear();
        }

        private void TouchEnded(CCTouch touch, CCEvent e)
        {
            var point = touch.Location;
            var square = point.GetSquare(this.piece.Board.Size);
            if (this.piece.MoveTo(square))
            {
                this.Position = square.GetPoint(this.piece.Board.Size);
            }
            else
            {
                this.Position = this.initPosition;
            }

            this.AnchorPoint = CCPoint.AnchorMiddle;
            this.Scale = this.ScaleX * 0.333f;
            drawNode.Clear();
            possibleMoves.Clear();
            possibleSquares.Clear();

        }

        private void Touch(CCTouch touch, CCEvent e)
        {
            var square = touch.Location.GetSquare(this.piece.Board.Size);
            if (!square.Equals(this.lastMovePosition))
            {
                drawNode.Clear();
                this.lastMovePosition = square;

                if (this.possibleSquares.Any(x => x.Equals(square)))
                {
                    var size = this.piece.Board.Size / 8;
                    var color = new CCColor4B(0, 199, 90, 0x66);
                    var point = square.GetPoint(this.piece.Board.Size);
                    drawNode.DrawSolidCircle(point, size, color);
                    drawNode.Visit();
                }
            }

            this.Position = touch.Location;
        }

        private CCPoint GetPosition()
        {
            return this.piece.Position.GetPoint(this.piece.Board.Size);
        }

        private bool SetPosition(CCPoint pos)
        {
            if (this.piece.MoveTo(pos.GetSquare(this.piece.Board.Size)))
            {
                Position = pos;
                return true;
            }

            return false;
        }

        public bool MoveToSquare(Square square)
        {
            return this.SetPosition(square.GetPoint(this.piece.Board.Size));
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            this.possibleMoves = new CCDrawNode();
            this.Parent.AddChild(this.possibleMoves, 1);

            drawNode = new CCDrawNode();
            this.Parent.AddChild(drawNode, 1);
        }
    }
}