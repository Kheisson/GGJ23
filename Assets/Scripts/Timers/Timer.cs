using System;

namespace Timers
{
    public class Timer
    {
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

        public void UpdateTimer(float deltaTime)
        {
            if (_isRunning)
            {
                _currentTime -= deltaTime;
            }
        }

        public bool IsTimerFinished()
        {
            return _currentTime <= 0;
        }

        public float GetTime()
        {
            return _currentTime;
        }
    }
}
