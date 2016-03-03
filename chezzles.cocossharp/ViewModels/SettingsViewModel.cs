using chezzles.cocossharp.Common;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

[assembly: Dependency(typeof(chezzles.cocossharp.ViewModels.SettingsViewModel))]
namespace chezzles.cocossharp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private ISetttingsProvider settings;

        public SettingsViewModel()
        {
            this.settings = DependencyService.Get<ISetttingsProvider>(DependencyFetchTarget.GlobalInstance);
        }

        public void Save()
        {
            // TODO: Save changes
        }
    }
}
