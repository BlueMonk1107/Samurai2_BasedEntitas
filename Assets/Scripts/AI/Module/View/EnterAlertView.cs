using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class EnterAlertView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.ENTER_ALERT;} }
        public override string AniName { get { return AIPeasantAniName.showSword.ToString(); } }

        public EnterAlertView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
