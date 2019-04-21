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
        }
    }
}
