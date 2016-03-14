using chezzles.cocossharp.ViewModels;
using System;

using Xamarin.Forms;

namespace chezzles.cocossharp
{
    public partial class Settings : ContentPage
    {
        private SettingsViewModel viewModel;

        public Settings()
        {
            InitializeComponent();
            this.BindingContext = this.viewModel = DependencyService.Get<SettingsViewModel>();            
        }

        public void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            this.viewModel.Save();
            this.Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel.InitializeCommand.Execute(null);
        }
    }
}

