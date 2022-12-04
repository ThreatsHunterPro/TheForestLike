using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Timer
{
    public class TimerManager : Singleton<TimerManager>
    {
        private event Action onTimerUpdated = null;

        void Update() => onTimerUpdated?.Invoke();

        public void Add(Action _callback, float _duration)
        {
            Timer _timer = new Timer(_callback, _duration, false);
            onTimerUpdated += _timer.IncreaseTimer;
            _timer.OnTimerEnded += Clear;
        }
        
        public void AddLoop(Action _callback, float _wait, float _duration)
        {
            Timer _timer = new Timer(_callback, _duration, true, _wait);
            onTimerUpdated += _timer.IncreaseTimer;
            _timer.OnTimerEnded += Clear;
        }

        public void Clear(Timer _timer)
        {
            _timer.OnTimerEnded -= Clear;
            onTimerUpdated -= _timer.IncreaseTimer;
        }
    }
}