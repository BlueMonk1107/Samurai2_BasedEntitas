using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class DeadView : ViewBase<ActionEnum>
    {
        public DeadView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
        public override ActionEnum Label { get { return ActionEnum.DEAD; } }

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
