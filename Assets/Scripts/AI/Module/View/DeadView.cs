using System;
using System.Threading.Tasks;
using BlueGOAP;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.AI.ViewEffect
{
    public abstract class DeadView : ViewBase<ActionEnum>
    {
        public DeadView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        protected void Destroy()
        {
            Transform self = _mgr.Self as Transform;
            GameObject.Destroy(self.gameObject);
        }
    }

    public class DeadNormalView : DeadView
    {
        public override ActionEnum Label { get { return ActionEnum.DEAD; } }
        public override string AniName {
            get
            {
                if (string.IsNullOrEmpty(_currentAniName))
                {
                    int index = Random.Range(0, _aniNames.Length);
                    _currentAniName = _aniNames[index].ToString();
                }
               
                return _currentAniName;
            }
        }

        private AIPeasantAniName[] _aniNames = { AIPeasantAniName.death01, AIPeasantAniName.death02};
        private string _currentAniName;

        public DeadNormalView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public async override void Enter()
        {
            base.Enter();
            await Task.Delay(TimeSpan.FromSeconds(_AniMgr.GetAniLength(AniName)));
            Destroy();
        }
    }

    public class DeadHeadView : DeadView
    {
        public DeadHeadView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_HEAD;} }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHHead.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
        }
    }

    public class DeadBodyView : DeadView
    {
        public DeadBodyView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_BODY; } }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHBody.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
        }
    }

    public class DeadLegView : DeadView
    {
        public DeadLegView(AIVIewEffectMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_LEG; } }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHLegs.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
        }
    }
}
