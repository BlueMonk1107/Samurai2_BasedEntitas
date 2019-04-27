using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class DeadView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD; } }
        public override string AniName { get; }

        public DeadView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
