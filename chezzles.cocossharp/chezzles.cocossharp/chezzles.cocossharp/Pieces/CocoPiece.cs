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

        public float radius { get { return ContentSize.Width * 0.5f; } }

        public PieceType PieceType
        {
            get { return piece.Type; }
        }

        public CocoPiece(Piece piece, CCSpriteFrame cCSpriteFrame) 
            : base(cCSpriteFrame)
        {
            this.piece = piece;
            this.piece.PieceTaken += OnPieceTaken;

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

            this.Scale = this.ScaleX * 2f;

            return true;
        }

        private void TouchCanceled(CCTouch touch, CCEvent e)
        {
            this.Position = this.initPosition;
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

            this.Scale = this.ScaleX * 0.5f;
        }

        private void Touch(CCTouch touch, CCEvent e)
        {
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
        }
    }
}