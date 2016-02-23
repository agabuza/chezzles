using chezzles.engine.Core.Game.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.ViewModels
{
    public class PuzzleViewModel : ViewModelBase
    {
        public int failedCount = 0;
        public int solvedCount = 0;

        public PuzzleViewModel(IMessenger messenger)
        {
            this.MessengerInstance = messenger;
            RegisterForMessages(messenger);
        }

        public string Status
        {
            get
            {
                return $"Solved: {this.solvedCount} Failed: {this.failedCount}";
            }
        }


        private void RegisterForMessages(IMessenger messenger)
        {
            MessengerInstance.Register<PuzzleCompletedMessage>(this, (msg) =>
            {
                if (msg.IsSolved)
                    this.solvedCount++;
                else
                    this.failedCount++;

                RaisePropertyChanged("Status");
            });
        }
    }
}
