using BlueGOAP;
using UnityEngine;

namespace Game.AI
{
    public class PeasasntTriggerMgr : TriggerManagerBase<ActionEnum, GoalEnum>
    {
        public PeasasntTriggerMgr(IAgent<ActionEnum, GoalEnum> agent) : base(agent)
        {
        }

        protected override void InitTriggers()
        {
            AddTrigger(new EyesTrigger(_agent));
            AddTrigger(new BodyDownTrigger(_agent));
            AddTrigger(new BodyLeftTrigger(_agent));
            AddTrigger(new BodyRightTrigger(_agent));
            AddTrigger(new BodyUpTrigger(_agent));
            AddTrigger(new BodyHeadTrigger(_agent));
            AddTrigger(new BodyBodyTrigger(_agent));
            AddTrigger(new BodyLegTrigger(_agent));
        }
    }
}
