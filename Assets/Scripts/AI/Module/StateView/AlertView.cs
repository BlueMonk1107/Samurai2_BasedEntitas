using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class AlertView : ViewBase<ActionEnum>
    {
        public AlertView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
            AlertModel model = _iModel as AlertModel;
            model.ShowSwordDuration = _AniMgr.GetAniLength(AIPeasantAniName.showSword);
            model.HideSwordDuration = _AniMgr.GetAniLength(AIPeasantAniName.hideSword);
        }

        public override ActionEnum Label { get { return ActionEnum.ALERT;} }

        public override void Enter()
        {
            base.Enter();
            _AniMgr.Play(AIPeasantAniName.showSword);
        }

        public override void Exit()
        {
            base.Exit();
            _AniMgr.Play(AIPeasantAniName.hideSword);
        }
      
    }
}
