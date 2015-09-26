using chezzles.cocossharp.Extensions;
using chezzles.engine.Core;
using CocosSharp;
using System;
using System.Collections.Generic;

namespace chezzles.cocossharp.Pieces
{
    public class CocoPiece : CCSprite
    {
        private Piece piece;
        private CCSprite sprite;
        private CCEventListenerTouchAllAtOnce touchListener;

        public float radius { get { return ContentSize.Width * 0.5f; } }

        public PieceType PieceType
        {
            get { return piece.Type; }
        }

        public CocoPiece(Piece piece, CCSpriteFrame cCSpriteFrame) 
            : base(cCSpriteFrame)
        {
            this.piece = piece;
            this.touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesMoved = HandleInput;
            AddEventListener(touchListener, this);
        }

        private void HandleInput(List<CCTouch> arg1, CCEvent arg2)
        {
        }

        public override CCPoint Position
        {
            get
            {
                return this.GetPosition();
            }

            set
            {
                SetPosition(value);
            }
        }

        private CCPoint GetPosition()
        {
            return this.Window == null ? base.Position : this.piece.GetPosition().GetPoint();
        }

        private bool SetPosition(CCPoint pos)
        {
            if (this.piece.MoveTo(pos.GetSquare(this)))
            {
                base.Position = pos;
                return true;
            }

            return false;
        }
    }
}