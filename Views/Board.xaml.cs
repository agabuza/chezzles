using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace chezzles
{
    public partial class Board : ContentPage
    {
        public Board()
        {
            InitializeComponent();

            for (int i = 1; i <= 8; i++)
                for (int j = 1; j <= 8; j++)
                {
                    var box = new BoxView();
					box.Color = (i + j) % 2 == 0 ? Color.White : Color.Navy;
                    box.HorizontalOptions = LayoutOptions.Fill;
                    box.VerticalOptions = LayoutOptions.Fill;                    

                    Grid.SetRow(box, i);
                    Grid.SetColumn(box, j);
                    this.board.Children.Add(box);
                }
        }
    }
}

