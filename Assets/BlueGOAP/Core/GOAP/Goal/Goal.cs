using System;
using System.Collections;

namespace BlueGOAP
{
    public abstract class GoalBase<TAction, TGoal> : IGoal<TGoal>
    {
        private IState _effects;
        private IAgent<TAction, TGoal> _agent;
        private Action<IGoal<TGoal>> _onActivate;
        private Action<IGoal<TGoal>> _onInactivate;

        public abstract TGoal Label { get;}

        public GoalBase(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
            _effects = InitEffects();
        }

        public abstract float GetPriority();

        /// <summary>
        /// 获取目标对状态的影响
        /// </summary>
        public IState GetEffects()
        {
            return _effects;
        }

        protected abstract IState InitEffects();

        /// <summary>
        /// 是否已经实现目标
        /// </summary>
        public virtual bool IsGoalComplete()
        {
            return _agent.AgentState.ContainState(_effects);
        }

        /// <summary>
        /// 获取代理状态的值
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool GetAgentState<Tkey>(Tkey key)
        {
            return _agent.AgentState.Get(key.ToString());
        }

        public void AddGoalActivateListener(Action<IGoal<TGoal>> onActivate)
        {
            _onActivate = onActivate;
        }

        public void AddGoalInactivateListener(Action<IGoal<TGoal>> onInactivate)
        {
            _onInactivate = onInactivate;
        }

        public void UpdateData()
        {
            DebugMsg.Log("----"+Label+"激活条件：" + ActiveCondition());

            if (ActiveCondition())
            {
                _onActivate(this);
            }
            else
            {
                _onInactivate(this);
            }
        }
        /// <summary>
        /// 当前Goal的激活条件
        /// </summary>
        /// <returns></returns>
        protected abstract bool ActiveCondition();
    }
}
