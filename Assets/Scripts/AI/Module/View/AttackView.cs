using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AttackView : ViewBase<ActionEnum>
    {
        public AttackView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
        public override ActionEnum Label { get { return ActionEnum.ATTACK; } }

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
