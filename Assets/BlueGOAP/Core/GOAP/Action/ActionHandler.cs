
using System;
using System.Threading.Tasks;

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
        protected IMaps<TAction, TGoal> _maps;
        private static int _id;
        protected int ID { get; set; }

        public ActionHandlerBase(IAgent<TAction, TGoal> agent, IMaps<TAction, TGoal> maps, IAction<TAction> action)
        {
            ID = _id ++;
            _agent = agent;
            _maps = maps;
            Action = action;
            ExcuteState = ActionExcuteState.INIT;
            _onFinishAction = null;
        }

        private void SetAgentData(IState state)
        {
            _agent.AgentState.Set(state);
        }

        protected void SetAgentState<TKey>(TKey key,bool value)
        {
            _agent.AgentState.Set(key.ToString(),value);
        }

        protected async void OnComplete(float delayTime = 0)
        {
            if(ExcuteState == ActionExcuteState.EXIT)
                return;

            await Task.Delay(TimeSpan.FromSeconds(delayTime));

            ExcuteState = ActionExcuteState.EXIT;

            DebugMsg.Log("------设置"+Label+"影响");
            if (Action.Effects != null)
                SetAgentData(Action.Effects);

            if (_onFinishAction != null)
                _onFinishAction();
        }

        public virtual bool CanPerformAction()
        {
            return Action.VerifyPreconditions();
        }

        public void AddFinishCallBack(Action onFinishAction)
        {
            _onFinishAction = onFinishAction;
        }

        protected virtual TClass GetGameData<TKey,TClass>(TKey key) where TKey : struct where TClass : class
        {
            return _maps.GetGameData<TKey, TClass>(key);
        }
        protected virtual TValue GetGameDataValue<TKey, TValue>(TKey key) where TKey : struct where TValue : struct 
        {
            return _maps.GetGameDataValue<TKey, TValue>(key);
        }
        protected virtual object GetGameData<TKey>(TKey key) where TKey : struct
        {
            return _maps.GetGameData(key);
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
