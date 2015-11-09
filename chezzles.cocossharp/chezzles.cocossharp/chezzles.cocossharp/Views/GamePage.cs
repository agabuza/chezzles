
using System;
using CocosSharp;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;

namespace chezzles.cocossharp.Views
{
    public class GamePage : ContentPage
    {
        CocosSharpView gameView;
        
        public GamePage()
        {
            this.gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                DesignResolution = new Size(100, 100),
                ViewCreated = LoadGame
            };
           
            Content = gameView;
        }

        private void LoadGame(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // Set world dimensions
                gameView.DesignResolution = new CCSizeI((int)this.Width, (int)this.Height);
                gameView.ContentManager.SearchPaths =
                    new List<string>() { "", "hd", "animations", "fonts", "sounds", "images" };

                CCScene gameScene = new CCScene(gameView);
                gameScene.AddLayer(new GameLayer(new CCSize((float)this.Width, (float)this.Height)));
                gameView.RunWithScene(gameScene);
            }
        }
    }
}
