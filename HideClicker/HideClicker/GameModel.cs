namespace HideClicker
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameModel : ReactiveObject
    {
        public GameModel()
        {

        }

        private bool _isStarted;
        public bool IsStarted
        {
            get => _isStarted;
            set => this.RaiseAndSetIfChanged(ref _isStarted, value);
        }

        private int _bossFightCounter;
        public int BossFightCounter
        {
            get => _bossFightCounter;
            set => this.RaiseAndSetIfChanged(ref _bossFightCounter, value);
        }

        private TimeSpan _resetTime;
        public TimeSpan ResetTime
        {
            get => _resetTime;
            set => this.RaiseAndSetIfChanged(ref _resetTime, value);
        }

        private TimeSpan _saveTime;
        public TimeSpan SaveTime
        {
            get => _saveTime;
            set => this.RaiseAndSetIfChanged(ref _saveTime, value);
        }

        private bool _isDpsMode;
        public bool IsDpsMode
        {
            get => _isDpsMode;
            set => this.RaiseAndSetIfChanged(ref _isDpsMode, value);
        }

        private bool _isDpsOnly = false;
        public bool IsDpsOnly
        {
            get => _isDpsOnly;
            set => this.RaiseAndSetIfChanged(ref _isDpsOnly, value);
        }

        private bool _isDpcOnly = false;
        public bool IsDpcOnly
        {
            get => _isDpcOnly;
            set => this.RaiseAndSetIfChanged(ref _isDpcOnly, value);
        }

        private bool _useBoosts = true;
        public bool UseBoosts
        {
            get => _useBoosts;
            set => this.RaiseAndSetIfChanged(ref _useBoosts, value);
        }
    }
}
