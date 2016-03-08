
using System;
using CocosSharp;
using Xamarin.Forms;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using chezzles.cocossharp.Messages;
using chezzles.engine.Core.Game.Messages;
using Acr.DeviceInfo;
using chezzles.cocossharp.Common;

namespace chezzles.cocossharp.Views
{
    // Wa can't ue xaml as Cocossharp doesn't want to integrate properly.
    // So stuck to incode creation of View.
    public class GamePage : ContentPage
    {
        private int solvedCount;
        private int failedCount;

        CocosSharpView gameView;
        private Label headerLabel;
        private Label moveLabel;
        private IMessenger messenger = Messenger.Default;
        private Button next;
        private Button skip;
        private GameLayer game;

        public GamePage()
        {
            BackgroundColor = BackgroundColor = Color.Silver;
            this.gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                // BUG: this needs to be initialized. 
                // Otherwise ContentSize of page will be negative.
                DesignResolution = new Size(1, 1),
                ViewCreated = LoadGame
            };

            ToolbarSetup();
            ControlsSetup();
            RegisterMessages();
        }

        private void ToolbarSetup()
        {
            var settings = new ToolbarItem
            {
                Icon = "/Resources/drawable/settings.png",
                Text = "Settings",
                Command = new Command(() => this.Navigation.PushAsync(new Settings())),
            };

            this.ToolbarItems.Add(settings);
        }

        private void ControlsSetup()
        {
            var grid = new Grid
            {
                Padding = new Thickness(2),
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = 85 },
                    new RowDefinition { Height = DeviceInfo.Hardware.ScreenWidth },
                    new RowDefinition { Height = 60 },
                }
            };

            this.headerLabel = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = $"Solved: {this.solvedCount} Failed: {this.failedCount}",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            grid.Children.Add(this.headerLabel, 0, 0);

            this.moveLabel = new Label
            {
                BackgroundColor = Color.FromRgba(22f, 41f, 247f, 0.45f),
                Text = $"Loading..",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var header = new StackLayout()
            {
                BackgroundColor = Color.Transparent,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    //this.headerLabel,
                    this.moveLabel
                }
            };

            this.next = new Button
            {
                Text = "Next",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Command = new RelayCommand(() => this.messenger.Send(new NextPuzzleMessage()))
            };

            this.skip = new Button
            {
                Text = "Skip",
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Command = new RelayCommand(() => this.messenger.Send(new SkipPuzzleMessage()))
            };

            var stack = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(0),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    skip,
                    next
                }
            };

            grid.Children.Add(header, 0, 1);
            grid.Children.Add(gameView, 0, 2);
            grid.Children.Add(stack, 0, 3);

            BackgroundImage = "felt.jpg";
            Content = grid;
        }

        private void RegisterMessages()
        {
            this.messenger.Register<PuzzleLoadedMessage>(this, (msg) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.next.IsEnabled = false;
                    this.moveLabel.TextColor = msg.IsWhiteMove ? Color.White : Color.Black;
                    this.moveLabel.Style = Device.Styles.SubtitleStyle;
                    this.moveLabel.Text = $"{(msg.IsWhiteMove ? "White" : "Black")} to move";
                });
            });

            this.messenger.Register<PuzzleCompletedMessage>(this, (msg) =>
            {
                if (msg.IsSolved)
                    this.solvedCount++;
                else
                    this.failedCount++;

                var color = msg.IsSolved ? Color.Green : Color.Red;
                var message = msg.IsSolved ? "Solved!" : "Incorrect";

                Device.BeginInvokeOnMainThread(() =>
                {
                    this.next.IsEnabled = true;
                    this.headerLabel.Text = $"Solved: {this.solvedCount} Failed: {this.failedCount}";
                    moveLabel.TextColor = color;
                    moveLabel.Text = message;
                });
            });
        }

        private void LoadGame(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                gameView.DesignResolution = new CCSizeI(600, 600);
                gameView.ContentManager.SearchPaths =
                    new List<string>() { "", "hd", "animations", "fonts", "sounds", "images" };

                CCScene gameScene = new CCScene(gameView);
                this.game = new GameLayer(new CCSize(600, 600));
                gameScene.AddLayer(game);
                gameView.RunWithScene(gameScene);

                Device.BeginInvokeOnMainThread(() =>
                {
                    this.moveLabel.Text = $"Press \"Next\" to start";
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.game.Save();
        }
    }
}
