using chezzles.cocossharp.ViewModels;
using chezzles.cocossharp.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace chezzles.cocossharp
{
	public partial class Settings : ContentPage
	{
        private SettingsViewModel viewModel;

        public Settings ()
		{
			InitializeComponent ();
            this.BindingContext = this.viewModel = DependencyService.Get<SettingsViewModel>();
        }

        public void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            this.viewModel.Save();
            this.Navigation.PopAsync();
        }
    }
}

