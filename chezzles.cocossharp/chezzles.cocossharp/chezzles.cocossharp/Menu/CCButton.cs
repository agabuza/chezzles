using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.Menu
{
    public class CCButton : CCNode
    {
        private CCLabel DisplayText { get; set; }

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != null && value != text && value.Length > 0)
                {
                    text = value;
                    RefreshText();
                }
            }
        }

        private int fontSize = 12;
        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                RefreshText();
            }
        }

        private void RefreshText()
        {
            if (DisplayText != null)
            {
                this.RemoveChild(DisplayText);
            }
            DisplayText = new CCLabel(text, "No Label", FontSize);
            DisplayText.Position = new CCPoint(ContentSize.Width / 2, ContentSize.Height / 2);
            this.AddChild(DisplayText, 2);
        }

        public CCButton(float width, float height)
        {
            Init(width, height);
        }

        public CCButton(float x, float y, float width, float height)
        {
            InitWithRect(x, y, width, height);
        }

        public virtual void Init(float width, float height)
        {
            EventListener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
                OnTouchEnded = OnTouchEnded,
            };
            ContentSize = new CCSize(width, height);
        }

        public virtual void InitWithRect(float x, float y, float width, float height)
        {
            Init(width, height);

            Position = new CCPoint(x, y);
        }

        public Action OnClick { get; set; }

        public CCEventListenerTouchOneByOne EventListener { get; set; }

        private bool OnTouchBegan(CCTouch touch, CCEvent e)
        {
            if (!Visible)
            {
                return false;
            }

            var rect = new CCRect(PositionWorldspace.X, PositionWorldspace.Y, ContentSize.Width, ContentSize.Height);
            if (!rect.ContainsPoint(touch.Location))
            {
                return false;
            }

            OnKeyDown();
            return true;
        }

        private void OnTouchEnded(CCTouch touch, CCEvent e)
        {
            OnKeyUp();
        }

        protected virtual void OnKeyDown()
        {
            OnClick();
        }

        protected virtual void OnKeyUp()
        {
        }
    }
}
