
namespace BlueGOAP
{
    public abstract class ActionBase<TAction, TGoal> : IAction<TAction>
    {
        public abstract TAction Label { get; }
        public abstract int Cost { get; }
        public abstract int Priority { get; }
        public abstract bool CanInterruptiblePlan { get; }

        /// <summary>
        /// 执行动作的先决条件
        /// </summary>
        public IState Preconditions { get; private set; }

        /// <summary>
        /// 动作执行后的状态
        /// </summary>
        public IState Effects { get; private set; }

        /// <summary>
        /// 当前动作的代理
        /// </summary>
        protected IAgent<TAction, TGoal> _agent;

        /// <summary>
        /// 当前动作是否能够中断
        /// </summary>
        protected bool _interruptible;

        public ActionBase(IAgent<TAction, TGoal> agent)
        {
            Preconditions = InitPreconditions();
            Effects = InitEffects();
            _agent = agent;
        }

        /// <summary>
        /// 初始化先决条件
        /// </summary>
        /// <returns></returns>
        protected abstract IState InitPreconditions();

        /// <summary>
        /// 初始化动作产生的影响
        /// </summary>
        /// <returns></returns>
        protected abstract IState InitEffects();

        /// <summary>
        /// 验证先决条件
        /// </summary>
        /// <returns></returns>
        public virtual bool VerifyPreconditions()
        {
            return _agent.AgentState.ContainState(Preconditions);
        }

    }
}
