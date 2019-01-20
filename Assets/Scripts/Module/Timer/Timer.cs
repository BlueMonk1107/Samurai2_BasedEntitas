using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Timer
{
    /// <summary>
    /// 计时器接口
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// 当前的时间
        /// </summary>
        float CurrentTime { get; }
        /// <summary>
        /// 运行百分比
        /// </summary>
        float Percent { get; }
        /// <summary>
        /// 单次循环持续时间
        /// </summary>
        float Duration { get; }
        /// <summary>
        /// 是否循环执行
        /// </summary>
        bool IsLoop { get; }
        /// <summary>
        /// 是否完成
        /// </summary>
        bool IsComplete { get; }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        void ResetData(float duration, bool loop);
        /// <summary>
        /// 帧函数
        /// </summary>
        void Update();
        /// <summary>
        /// 继续计时
        /// </summary>
        void Continue();
        /// <summary>
        /// 暂停计时
        /// </summary>
        void Pause();
        /// <summary>
        /// 停止计时
        /// </summary>
        void Stop();

        void AddUpdateListener(Action update);
        void AddCompleteListener(Action complete);
    }

    public interface ITimerManager
    {
        /// <summary>
        /// 创建计时器
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        ITimer CreateTimer(float duration, bool loop);
        /// <summary>
        /// 帧函数
        /// </summary>
        void Update();
        /// <summary>
        /// 继续执行所有计时器
        /// </summary>
        void ContinueAll();

        /// <summary>
        /// 暂停所有计时器
        /// </summary>
        void PauseAll();
        /// <summary>
        /// 关闭所有计时器
        /// </summary>
        void StopAll();
    }

    /// <summary>
    /// 计时器管理类
    /// </summary>
    public class TimerManager : ITimerManager
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private class Timer : ITimer
        {
            /// <summary>
            /// 当前的时间
            /// </summary>
            public float CurrentTime
            {
                get { return _runTimeTotal; }
            }

            /// <summary>
            /// 运行百分比
            /// </summary>
            public float Percent
            {
                get { return _runTimeTotal / _duration; }
            }

            /// <summary>
            /// 单次循环持续时间
            /// </summary>
            public float Duration { get { return _duration; } }
            //是否完成
            public bool IsComplete
            {
                get { return _runTimeTotal >= _duration; }
            }
            //是否循环执行
            public bool IsLoop { get; private set; }

            private Action _onUpdate;
            private Action _onComplete;
            //是否正在计时
            private bool _isTiming;
            //计时开始时间
            private DateTime _startTime;
            //总运行时间
            private float _runTimeTotal;
           
            //持续时间
            private float _duration;

            public Timer(float duration, bool loop)
            {
                InitData(duration, loop);
            }


            private void InitData(float duration, bool loop)
            {
                _duration = duration;
                IsLoop = loop;
                ResetData();
            }

            public void ResetData(float duration, bool loop)
            {
                InitData(duration, loop);
            }

            private void ResetData()
            {
                _isTiming = true;
                _startTime = DateTime.Now;
                _runTimeTotal = 0;
            }

            public void Update()
            {
                if (!IsComplete || !_isTiming)
                    return;

                if (IsLoop)
                {
                    Loop();
                }
                else
                {
                    NotLoop();
                }

                _onUpdate?.Invoke();
            }

            private void Loop()
            {
                if (IsComplete)
                {
                    _onComplete?.Invoke();
                    ResetData();
                }
            }

            private void NotLoop()
            {
                if (IsComplete)
                {
                    _onComplete?.Invoke();
                }
            }

            public void Continue()
            {
                _isTiming = true;
                _startTime = DateTime.Now;
            }

            public void Pause()
            {
                _isTiming = false;
                _runTimeTotal += GetCurrentTimingTime();
            }

            public void Stop()
            {
                if (IsComplete)
                {
                    _onComplete?.Invoke();
                }
                _runTimeTotal = 0;
                _isTiming = false;
            }

            public void AddUpdateListener(Action update)
            {
                _onUpdate += update;
            }

            public void AddCompleteListener(Action complete)
            {
                _onComplete += complete;
            }

            private float GetCurrentTimingTime()
            {
                var time = DateTime.Now - _startTime;
                return (float)time.TotalSeconds;
            }
        }

        private HashSet<ITimer> _activeTimers;
        private HashSet<ITimer> _inactiveTimers;

        public TimerManager()
        {
            _activeTimers = new HashSet<ITimer>();
            _inactiveTimers = new HashSet<ITimer>();
        }

        /// <summary>
        /// 创建新计时器
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        public ITimer CreateTimer(float duration, bool loop)
        {
            ITimer timer = null;
            if (_inactiveTimers.Count >= 0)
            {
                timer = _inactiveTimers.First();
                _inactiveTimers.Remove(timer);
                timer.ResetData(duration,loop);
                _activeTimers.Add(timer);
            }
            else
            {
                timer = new Timer(duration, loop);
                _activeTimers.Add(timer);
            }

            return timer;
        }

        public void Update()
        {
            if (_activeTimers.Count > 0)
            {
                foreach (ITimer timer in _activeTimers)
                {
                    timer.Update();
                    SetInactiveTimer(timer);
                }
            }
        }

        /// <summary>
        /// 获取执行完毕的计时器，存入缓存
        /// </summary>
        /// <param name="timer"></param>
        private void SetInactiveTimer(ITimer timer)
        {
            if (!timer.IsLoop && timer.IsComplete)
            {
                _activeTimers.Remove(timer);
                _inactiveTimers.Add(timer);
            }
        }

        /// <summary>
        /// 继续执行所有计时器
        /// </summary>
        public void ContinueAll()
        {
            foreach (ITimer timer in _activeTimers)
            {
                timer.Continue();
            }
        }

        /// <summary>
        /// 暂停所有计时器
        /// </summary>
        public void PauseAll()
        {
            foreach (ITimer timer in _activeTimers)
            {
                timer.Pause();
            }
        }

        /// <summary>
        /// 关闭所有计时器
        /// </summary>
        public void StopAll()
        {
            foreach (ITimer timer in _activeTimers)
            {
                timer.Stop();
            }
        }


    }
}
