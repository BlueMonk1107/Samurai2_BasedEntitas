using Entitas;
using Module.Timer;
using UnityEngine;

namespace Game.Service
{
    /// <summary>
    /// 计时器服务接口
    /// </summary>
    public interface ITimerService : IInitService, IExecuteService, ITimerManager
    {
        /// <summary>
        /// 创建计时器，如当前指定名称计时器正在计时，返回null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        ITimer CreateTimer(TimerId id, float duration, bool loop);
        /// <summary>
        /// 重置指定ID的Timer数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        ITimer ResetTimerData(TimerId id, float duration, bool loop);
        /// <summary>
        /// 指定ID的timer为空，创建timer，不为空，重新启动timer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        ITimer CreatOrRestartTimer(TimerId id, float duration, bool loop);
        ITimer GeTimer(TimerId id);
    }

    public class TimerService : ITimerService
    {
        private ITimerManager _timerManager;

        public TimerService(ITimerManager manager)
        {
            _timerManager = manager;
        }

        public void Init(Contexts contexts)
        {
            contexts.service.SetGameServiceTimerService(this);
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Excute()
        {
            Update();
        }

        public ITimer CreateTimer(TimerId id,float duration, bool loop)
        {
            return CreateTimer(id.ToString(),duration, loop);
        }

        public ITimer GeTimer(TimerId id)
        {
            return GeTimer(id.ToString());
        }

        public ITimer CreateTimer(string id, float duration, bool loop)
        {
            return _timerManager.CreateTimer(id, duration, loop);
        }

        public ITimer ResetTimerData(TimerId id, float duration, bool loop)
        {
            return ResetTimerData(id.ToString(), duration, loop);
        }

        public ITimer ResetTimerData(string id, float duration, bool loop)
        {
            return _timerManager.ResetTimerData(id,duration,loop);
        }
        public ITimer CreatOrRestartTimer(TimerId id, float duration, bool loop)
        {
            return CreatOrRestartTimer(id.ToString(), duration, loop);
        }

        public ITimer CreatOrRestartTimer(string id, float duration, bool loop)
        {
            return _timerManager.CreatOrRestartTimer(id, duration, loop);
        }

        public ITimer GeTimer(string id)
        {
            return _timerManager.GeTimer(id);
        }

        public void StopTimer(ITimer timer, bool isComplete)
        {
            _timerManager.StopTimer(timer,isComplete);
        }

        public void Update()
        {
            _timerManager.Update();
        }

        public void ContinueAll()
        {
            _timerManager.ContinueAll();
        }

        public void PauseAll()
        {
            _timerManager.PauseAll();
        }

        public void StopAll()
        {
            _timerManager.StopAll();
        }
    }
}
