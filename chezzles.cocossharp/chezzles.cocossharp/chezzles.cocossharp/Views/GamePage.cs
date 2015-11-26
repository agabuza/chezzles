
using System;
using CocosSharp;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using chezzles.cocossharp.Messages;
using chezzles.engine.Core.Game.Messages;

namespace chezzles.cocossharp.Views
{
    public class GamePage : ContentPage
    {
        private int solvedCount;
        private int failedCount;

        CocosSharpView gameView;
        private Label label;
        private IMessenger messenger = Messenger.Default;

        public GamePage()
        {
            this.gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                // BUG: this needs to be initialized. 
                // Otherwise ContentSize of page will be negative.
                DesignResolution = new Size(1, 1),
                ViewCreated = LoadGame
            };

            var grid = new Grid
            {
                BackgroundColor = Color.FromHex("#899AE0"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = 50 }
                }
            };

            grid.Children.Add(
                this.label = new Label
            {
                Text = $"Solved: {this.solvedCount} Failed: {this.failedCount}",
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Droid Sans Mono"
            }, 0, 0);

            grid.Children.Add(gameView, 0, 1);

            var stack = new StackLayout()
            {
                Padding = new Thickness(5, 5, 5, 5),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Button
                    {
                        Text = "Skip",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Command = new RelayCommand(() => this.messenger.Send<SkipPuzzleMessage>(new SkipPuzzleMessage()) )
                    },
                    new Button
                    {
                        Text = "Next",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Command = new RelayCommand(() => this.messenger.Send<NextPuzzleMessage>(new NextPuzzleMessage()) )
                    }
                }
            };

            grid.Children.Add(stack, 0, 2);
            Content = grid;

            this.RegisterMessages();
        }

        private void RegisterMessages()
        {
            this.messenger.Register<PuzzleFailedMessage>(this, (msg) =>
            {
                this.failedCount++;
                //this.label.Text = $"Solved: {this.solvedCount} Failed: {this.failedCount}";
            });

            this.messenger.Register<PuzzleSolvedMessage>(this, (msg) =>
            {
                this.solvedCount++;
                //this.label.Text = $"Solved: {this.solvedCount} Failed: {this.failedCount}";
            });
        }

        private void LoadGame(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // Set world dimensions
                gameView.DesignResolution = new CCSizeI((int)600, (int)600);
                gameView.ContentManager.SearchPaths =
                    new List<string>() { "", "hd", "animations", "fonts", "sounds", "images" };

                CCScene gameScene = new CCScene(gameView);
                gameScene.AddLayer(new GameLayer(new CCSize((float)600, (float)600)));
                gameView.RunWithScene(gameScene);
            }
        }
    }
}
