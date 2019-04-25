using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class InjureView : ViewBase<ActionEnum>
    {
        public InjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
        public override ActionEnum Label { get { return ActionEnum.INJJURE; } }

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
