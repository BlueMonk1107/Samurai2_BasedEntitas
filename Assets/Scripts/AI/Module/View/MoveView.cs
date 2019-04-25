using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class MoveView : ViewBase<ActionEnum>
    {
        public MoveView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
        public ActionExcuteState ExcuteState { get; }
        public override ActionEnum Label { get { return ActionEnum.MOVE; } }

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
