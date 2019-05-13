using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class MoveBackwardView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.MOVE_BACKWARD; } }
        public override string AniName { get { return AIPeasantAniName.runSwordBackward.ToString(); } }

        public MoveBackwardView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
     
    }
}
