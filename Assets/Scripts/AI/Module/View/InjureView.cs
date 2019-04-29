using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class InjureView : ViewBase<ActionEnum>
    {
        public InjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }

    public class UpInjureView : InjureView
    {
        public override ActionEnum Label { get {return ActionEnum.INJURE_UP;} }
        public override string AniName { get { return AIPeasantAniName.injuryFront03.ToString(); } }

        public UpInjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class DownInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_DOWN; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront04.ToString(); } }

        public DownInjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class LeftInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_LEFT; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront02.ToString(); } }

        public LeftInjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
    public class RightInjureView : InjureView
    {
        public override ActionEnum Label { get { return ActionEnum.INJURE_RIGHT; } }
        public override string AniName { get { return AIPeasantAniName.injuryFront01.ToString(); } }

        public RightInjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
