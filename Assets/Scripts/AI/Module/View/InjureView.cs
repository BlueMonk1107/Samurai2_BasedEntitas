using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class InjureView : ViewBase<ActionEnum>
    {
        private EffectNameEnum[] _effects = {EffectNameEnum.InjureLeft, EffectNameEnum.InjureRight};

        public InjureView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Transform self = _mgr.Self as Transform;
            Vector3 pos = self.GetComponent<CharacterController>().center + self.position;
            _effectMgr.Play(GetEffectName(), pos);
            _effectMgr.Play(EffectNameEnum.BloodOnGround, self.position);
            _AudioMgr.Play(AudioNameEnum.injory, AudioVolumeConst.INJURE_VOLUME);
        }

        private EffectNameEnum GetEffectName()
        {
            int index = Random.Range(0, _effects.Length);
            return _effects[index];
        }
    }

    public class UpInjureView : InjureView
    {
        public override ActionEnum Label { get {return ActionEnum.INJURE_UP;} }
        public override string AniName { get { return AIPeasantAniName.injuryFront03.ToString(); } }

        public UpInjureView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class DownInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_DOWN; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront04.ToString(); } }

        public DownInjureView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class LeftInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_LEFT; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront02.ToString(); } }

        public LeftInjureView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class RightInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_RIGHT; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront01.ToString(); } }

        public RightInjureView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
