
using System;

namespace BlueGOAP
{
    public abstract class ActionHandlerBase<TAction, TGoal> : IActionHandler<TAction>
    {
        /// <summary>
        /// 动作
        /// </summary>
        public IAction<TAction> Action { get; private set; }

        public TAction Label
        {
            get { return Action.Label; }
        }

        public ActionExcuteState ExcuteState { get; private set; }

        protected IAgent<TAction, TGoal> _agent;
        private IAction<TAction> action;
        protected System.Action _onFinishAction;
        /// <summary>
        /// 是否需要重置先置条件（就是先置条件全部取反）默认为true
        /// </summary>
        protected bool IsNeedResetPreconditions;

        public ActionHandlerBase(IAgent<TAction, TGoal> agent, IAction<TAction> action)
        {
            _agent = agent;
            Action = action;
            ExcuteState = ActionExcuteState.INIT;
            _onFinishAction = null;
            IsNeedResetPreconditions = true;
        }

        private void SetAgentData(IState state)
        {
            _agent.AgentState.Set(state);
        }

        protected void SetAgentState<TKey>(TKey key,bool value)
        {
            _agent.AgentState.Set(key.ToString(),value);
        }

        protected void OnComplete()
        {
            ExcuteState = ActionExcuteState.EXIT;

            if (_onFinishAction != null)
                _onFinishAction();

            SetAgentData(Action.Effects);

            if (IsNeedResetPreconditions)
                SetAgentData(Action.Preconditions.InversionValue());
        }

        public bool CanPerformAction()
        {
            DebugMsg.Log("Action:"+ Action.Label);
            return Action.VerifyPreconditions();
        }

        public void AddFinishCallBack(Action onFinishAction)
        {
            _onFinishAction = onFinishAction;
        }

        protected object GetGameData<TKey>(TKey key)
        {
            return _agent.Maps.GetGameData(key);
        }

        public virtual void Enter()
        {
            ExcuteState = ActionExcuteState.ENTER;
        }

        public virtual void Execute()
        {
            ExcuteState = ActionExcuteState.EXCUTE;
        }

        public virtual void Exit()
        {
            if (ExcuteState != ActionExcuteState.EXIT)
            {
                OnComplete();
            }
        }

    }
}
