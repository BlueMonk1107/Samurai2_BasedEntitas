using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AttackView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.ATTACK; } }
        public override string AniName { get { return AIPeasantAniName.attackPeasant.ToString(); } }

        public AttackView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
