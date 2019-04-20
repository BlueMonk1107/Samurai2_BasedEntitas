
using System.Collections.Generic;

namespace BlueGOAP
{
    public interface ITriggerManager    
    {
        /// <summary>
        /// 帧函数
        /// </summary>
        void FrameFun();
    }

    public abstract class TriggerManagerBase<TAction, TGoal> : ITriggerManager
    {
        protected IAgent<TAction, TGoal> _agent;
        private HashSet<ITrigger> _triggers; 

        public TriggerManagerBase(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
            _triggers = new HashSet<ITrigger>();
            InitTriggers();
        }

        public void FrameFun()
        {
            foreach (ITrigger trigger in _triggers)
            {
                trigger.FrameFun();
            }
        }

        protected void AddTrigger(ITrigger trigger)
        {
            _triggers.Add(trigger);
        }
        /// <summary>
        /// 所有需要添加的触发器在此函数内进行添加
        /// </summary>
        protected abstract void InitTriggers();
    }
}
