using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class IdleView : ViewBase<ActionEnum>
    {
        public IdleView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
        public override ActionEnum Label { get { return ActionEnum.MOVE_BACKWARD; } }

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
