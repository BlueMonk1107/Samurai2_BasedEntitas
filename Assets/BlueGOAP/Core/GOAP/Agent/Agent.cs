
using System;

namespace BlueGOAP
{
    public abstract class AgentBase<TAction, TGoal> : IAgent<TAction, TGoal>
        where TAction : struct
        where TGoal : struct
    {
        public IState AgentState { get; private set; }
        public IMaps<TAction, TGoal> Maps { get; protected set; }
        public IActionManager<TAction> ActionManager { get; protected set; }
        public IGoalManager<TGoal> GoalManager { get; private set; }
        public IPerformer Performer { get; private set; }

        private ITriggerManager _triggerManager;
        protected Action<IAgent<TAction, TGoal>, IMaps<TAction, TGoal>> _onInitGameData;

        public AgentBase(Action<IAgent<TAction, TGoal>, IMaps<TAction, TGoal>> onInitGameData)
        {
            _onInitGameData = onInitGameData;
            DebugMsgBase.Instance = InitDebugMsg();
            AgentState = InitAgentState();
            Maps = InitMaps();
            ActionManager = InitActionManager();
            GoalManager = InitGoalManager();
            _triggerManager = InitTriggerManager();
            Performer = new Performer<TAction, TGoal>(this);
            
            AgentState.AddStateChangeListener(UpdateData);

            JudgeException(Maps, "Maps");
            JudgeException(ActionManager, "ActionManager");
            JudgeException(GoalManager, "GoalManager");
            JudgeException(_triggerManager, "_triggerManager");
        }

        private void JudgeException(object obj, string name)
        {
            if (obj == null)
            {
                DebugMsg.LogError("代理中" + name + "对象为空,请在代理子类中初始化该对象");
            }
        }

        protected abstract IState InitAgentState();
        protected abstract IMaps<TAction, TGoal> InitMaps();
        protected abstract IActionManager<TAction> InitActionManager();
        protected abstract IGoalManager<TGoal> InitGoalManager();
        protected abstract ITriggerManager InitTriggerManager();
        protected abstract DebugMsgBase InitDebugMsg();

        public void UpdateData()
        {
            if (ActionManager != null)
                ActionManager.UpdateData();

            if (GoalManager != null)
                GoalManager.UpdateData();

            Performer.UpdateData();
        }

        public virtual void FrameFun()
        {
            if (_triggerManager != null)
                _triggerManager.FrameFun();

            if (ActionManager != null)
                ActionManager.FrameFun();
        }
    }
}
