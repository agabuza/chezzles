using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.Menu
{
    public delegate void ClickEventArgs(object sender);

    public class CCButton : CCSprite
    {
        private CCEventListenerTouchOneByOne touchListener;
        private float initialScale;

        public CCButton(float width, float height)
        {
            Init(width, height);
        }

        public CCButton(float x, float y, float width, float height)
        {
            InitWithRect(x, y, width, height);
        }

        public CCButton(CCSpriteFrame frame)
            : base(frame)
        {
            Init(frame.ContentSize.Width, frame.ContentSize.Height);
        }

        public virtual void Init(float width, float height)
        {
            ContentSize = new CCSize(width, height);
            touchListener = new CCEventListenerTouchOneByOne();

            touchListener.OnTouchBegan = ToucheBegan;
            touchListener.OnTouchEnded = TouchEnded;
            AddEventListener(touchListener, this);
        }

        public event ClickEventArgs Click;

        private void TouchEnded(CCTouch touch, CCEvent e)
        {
            this.Scale = this.initialScale;
            if (this.Click != null)
            {
                this.Click(this);
            }
        }

        private bool ToucheBegan(CCTouch touch, CCEvent e)
        {
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

            this.initialScale = this.ScaleX;
            this.Scale = this.initialScale * 0.9f;

            return true;
        }

        public virtual void InitWithRect(float x, float y, float width, float height)
        {
            Init(width, height);
            Position = new CCPoint(x, y);
        }

        public Action OnClick { get; set; }
    }
}
