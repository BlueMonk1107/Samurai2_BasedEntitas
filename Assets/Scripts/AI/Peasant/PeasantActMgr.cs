using System;
using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class PeasantActMgr : ActionManagerBase<ActionEnum, GoalEnum>
    {
        private Action<ActionEnum> _excuteActionState;

        public PeasantActMgr(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        public override ActionEnum GetDefaultActionLabel()
        {
            return ActionEnum.IDLE;
        }

        protected override void InitActionHandlers()
        {
            AddHandler(ActionEnum.IDLE);
            AddHandler(ActionEnum.IDLE_SWORD);
            AddHandler(ActionEnum.MOVE);
            AddHandler(ActionEnum.MOVE_BACKWARD);
            AddHandler(ActionEnum.ATTACK);
            AddHandler(ActionEnum.INJJURE);
            AddHandler(ActionEnum.DEAD);
        }

        protected override void InitActionStateHandlers()
        {
            AddMutilActionHandler(ActionEnum.ALERT);
        }

        public void AddExcuteNewStateListener(Action<ActionEnum> excuteActionState)
        {
            _excuteActionState = excuteActionState;
        }

        public override void ExcuteNewState(ActionEnum label)
        {
            base.ExcuteNewState(label);
            if(_excuteActionState != null)
                _excuteActionState(label);
        }
    }
}
