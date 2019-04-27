using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public class InjureView : ViewBase<ActionEnum>
    {
        public override ActionEnum Label { get { return ActionEnum.INJJURE; } }
        public override string AniName { get; }

        public InjureView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }
    }
}
