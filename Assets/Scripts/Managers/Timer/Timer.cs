using System;
using UnityEngine;

namespace Managers.Timer
{
    public class Timer
    {
        public event Action<Timer> OnTimerEnded = null;

        private readonly bool isLoop = false;
        private readonly float duration = 0.0f;
        private float currentDuration = 0.0f;
        private readonly float waitTime = 0.0f;
        private float currentWaitTime = 0.0f;
        private readonly Action callback = null;

        public Timer(Action _callback, float _duration, bool _isLoop = false, float _waitTime = 0.0f)
        {
            callback = _callback;
            duration = _duration;
            isLoop = _isLoop;
            waitTime = _waitTime;
        }

        public void IncreaseTimer()
        {
            if (isLoop && currentWaitTime < waitTime)
            {
                currentWaitTime += Time.deltaTime;
                return;
            }
            
            currentDuration += Time.deltaTime;
            if (currentDuration > duration)
            {
                callback?.Invoke();

                if (!isLoop)
                {
                    OnTimerEnded?.Invoke(this);
                }

                currentDuration = 0.0f;
            }
        }
    }
}
