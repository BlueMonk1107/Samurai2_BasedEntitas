using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class ExitAlertView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.EXIT_ALERT;} }
        public override string AniName { get { return AIPeasantAniName.hideSword.ToString(); } }

        public ExitAlertView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
