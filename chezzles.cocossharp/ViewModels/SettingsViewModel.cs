using chezzles.cocossharp.Common;
using chezzles.cocossharp.Pieces.Model;
using chezzles.cocossharp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

[assembly: Dependency(typeof(chezzles.cocossharp.ViewModels.SettingsViewModel))]
namespace chezzles.cocossharp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private const string CHESS_SET = "chess-set";
        private ISetttingsProvider settings;
        private IRestService<ChessSet> service;
        private List<ChessSet> chessSets;
        private bool isBusy;
        private ICommand initializeCommand;
        private ChessSet selectedChessSet;

        public SettingsViewModel()
        {
            this.chessSets = new List<ChessSet>();
            this.selectedChessSet = null;
        }

        public ICommand InitializeCommand
        {
            get
            {
                return this.initializeCommand ?? (this.initializeCommand = new RelayCommand(
                    () =>
                    {
                        this.IsBusy = true;
                        Task.Run(OnInitialized);
                    }));
            }
        }

        protected async Task OnInitialized()
        {
            this.ChessSets = await this.Service.GetAll();

            var set = Settings[CHESS_SET];
            var chessSet = JsonConvert.DeserializeObject<ChessSet>(set);
            if (chessSet != null)
            {
                this.SelectedChessSet = this.ChessSets.FirstOrDefault(x => x.FilePath == chessSet.FilePath);
            }

            this.IsBusy = false;
        }

        public IRestService<ChessSet> Service
        {
            get
            {
                if (this.service == null)
                {
                    this.service = DependencyService.Get<IRestService<ChessSet>>();
                }

                return this.service;
            }

            internal set
            {
                this.service = value;
            }
        }

        public List<ChessSet> ChessSets
        {
            get
            {
                return this.chessSets;
            }

            set
            {
                this.chessSets = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged();
            }
        }

        public ISetttingsProvider Settings
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = DependencyService.Get<ISetttingsProvider>(DependencyFetchTarget.GlobalInstance);
                }

                return this.settings;
            }

            internal set
            {
                this.settings = value;
            }
        }

        public ChessSet SelectedChessSet
        {
            get
            {
                return this.selectedChessSet;
            }

            set
            {
                this.selectedChessSet = value;
                this.RaisePropertyChanged();
            }
        }

        public void Save()
        {
            this.Settings[CHESS_SET] = JsonConvert.SerializeObject(SelectedChessSet);
            this.Settings.SaveAsync();
        }
    }
}
