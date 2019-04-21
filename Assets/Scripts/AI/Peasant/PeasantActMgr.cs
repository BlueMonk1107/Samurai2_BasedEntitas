using System;
using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class PeasantActMgr : ActionManagerBase<ActionEnum, GoalEnum>
    {
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

        protected override void InitMutilActionHandlers()
        {
            AddMutilActionHandler(ActionEnum.ALERT);
        }
    }
}
