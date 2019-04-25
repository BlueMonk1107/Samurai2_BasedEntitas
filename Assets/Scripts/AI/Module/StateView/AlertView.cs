using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AlertView : ViewBase<ActionEnum>
    {
        public AlertView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.ALERT;} }

        public override void Enter()
        {
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
      
    }
}
