

namespace BlueGOAP
{
    /// <summary>
    /// 触发器接口
    /// 用于响应环境对AI的影响
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// 是否触发
        /// </summary>
        bool IsTrigger { get; set; }
        /// <summary>
        /// 帧函数
        /// </summary>
        void FrameFun();
    }

    public abstract class TriggerBase<TAction, TGoal> : ITrigger
    {
        private IState _effects;
        private IAgent<TAction, TGoal> _agent;

        public TriggerBase(IAgent<TAction, TGoal> agent)
        {
            _effects = InitEffects();
            _agent = agent;
        }

        public void FrameFun()
        {
            if (IsTrigger)
            {
                _agent.AgentState.Set(_effects);
            }
        }

        /// <summary>
        /// 判断是否满足触发条件
        /// </summary>
        public abstract bool IsTrigger { get; set; }

        protected abstract IState InitEffects();
    }
}
