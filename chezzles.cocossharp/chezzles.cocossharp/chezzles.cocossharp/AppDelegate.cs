using System.Reflection;
using Microsoft.Xna.Framework;
using CocosSharp;
using CocosDenshion;

namespace chezzles.cocossharp
{
    public class AppDelegate : CCApplicationDelegate
    {
        public CCSize DefaultResolution { get; private set; }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            application.PreferMultiSampling = false;
            application.ContentRootDirectory = "Content";
            application.ContentSearchPaths.Add("animations");
            application.ContentSearchPaths.Add("fonts");
            application.ContentSearchPaths.Add("sounds");
            application.ContentSearchPaths.Add("images");

            DefaultResolution = new CCSize(
            application.MainWindow.WindowSizeInPixels.Width,
            application.MainWindow.WindowSizeInPixels.Height);

            // This will set the world bounds to be (0,0, w, h)
            // CCSceneResolutionPolicy.ShowAll will ensure that the aspect ratio is preserved
            //CCScene.SetDefaultDesignResolution(desiredWidth, desiredHeight, CCSceneResolutionPolicy.ShowAll);

            // Determine whether to use the high or low def versions of our images
            // Make sure the default texel to content size ratio is set correctly
            // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
            //if (desiredWidth < windowSize.Width)
            //{
            //    application.ContentSearchPaths.Add("hd");
            //    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
            //}
            //else
            //{
            //    application.ContentSearchPaths.Add("ld");
            //    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
            //}

            var scene = new CCScene(mainWindow);
            
            var introLayer = new GameLayer(DefaultResolution);
            introLayer.AnchorPoint = CCPoint.AnchorLowerLeft;
            introLayer.Position = new CCPoint(0, 0);
            scene.AddChild(introLayer);

            mainWindow.RunWithScene(scene);
        }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            application.Paused = true;
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            application.Paused = false;
        }
    }
}