using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class IdleView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.IDLE; } }
        public override string AniName { get { return AIPeasantAniName.idle.ToString(); } }

        public IdleView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
