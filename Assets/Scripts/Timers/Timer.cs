using System;
using UnityEngine;

namespace Timers
{
    public class Timer
    {
        public event Action OnTimerEnd;
        
        private float _time;
        private float _currentTime;
        private bool _isRunning;

        public Timer(float time)
        {
            _time = time;
            _currentTime = time;
        }

        public void StartTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResetTimer()
        {
            _currentTime = _time;
        }

        public void UpdateTimer()
        {
            if (_isRunning)
            {
                _currentTime -= Time.deltaTime;
            }

            if (!(_currentTime <= 0))
            {
                return;
            }
            
            //Update if timer is finished and stop + reset
            _currentTime = 0;
            StopTimer();
            ResetTimer();
            OnTimerEnd?.Invoke();
        }

        public bool IsTimerFinished()
        {
            return _currentTime <= 0;
        }

        public float GetTime()
        {
            return _currentTime;
        }

        public void ChangeTime(float newTime)
        {
            _time = newTime;
        }
    }
}
