using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class IdleView : ViewBase<ActionEnum>
    {
        public IdleView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.IDLE; } }

        public override void Enter()
        {
            base.Enter();
            _AniMgr.Play(AIPeasantAniName.idle);
        }
    }
}
