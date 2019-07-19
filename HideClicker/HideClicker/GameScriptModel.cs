namespace HideClicker
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameScriptModel : ReactiveObject
    {
        public GameScriptModel()
        {

        }

        private bool _isStarted;
        public bool IsStarted
        {
            get => _isStarted;
            set => this.RaiseAndSetIfChanged(ref _isStarted, value);
        }

        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set => this.RaiseAndSetIfChanged(ref _isPaused, value);
        }

        private string _status;
        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
    }
}
