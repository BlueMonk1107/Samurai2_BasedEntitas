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
        private Action _onExcuteAfterAni;

        public DeadView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
            _onExcuteAfterAni = null;
        }

        public override void Enter()
        {
            base.Enter();
            _AudioMgr.Play(AudioNameEnum.death, AudioVolumeConst.DEAD_VOLUME);
            ExcuteAfterAni();
        }

        protected void Destroy()
        {
            Transform self = _mgr.Self as Transform;
            if(self != null)
                GameObject.Destroy(self.gameObject);
        }

        protected async void ExcuteAfterAni()
        {
            await Task.Delay(TimeSpan.FromSeconds(_aniMgr.GetAniLength(AniName)));
            if(_onExcuteAfterAni != null)
                _onExcuteAfterAni();
        }

        protected void AddExcuteAfterAniListener(Action excuteAfterAni)
        {
            _onExcuteAfterAni = excuteAfterAni;
        }

        protected void DeadEffect()
        {
            Vector3 pos = (_mgr.Self as Transform).position;
            _effectMgr.Play(EffectNameEnum.Dead, pos);
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

        public DeadNormalView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override void Enter()
        {
            base.Enter();
            AddExcuteAfterAniListener(() =>
            {
                Destroy();
                DeadEffect();
            });
        }
    }

    public class DeadHeadView : DeadView
    {
        public DeadHeadView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_HEAD;} }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHHead.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
            AddExcuteAfterAniListener(DeadEffect);
        }
    }

    public class DeadBodyView : DeadView
    {
        public DeadBodyView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_BODY; } }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHBody.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
            AddExcuteAfterAniListener(DeadEffect);
        }
    }

    public class DeadLegView : DeadView
    {
        public DeadLegView(AiViewMgrBase<ActionEnum> mgr) : base(mgr)
        {
        }

        public override ActionEnum Label { get { return ActionEnum.DEAD_HALF_LEG; } }
        public override string AniName { get { return AIPeasantAniName.DeadPeasantHLegs.ToString(); } }

        public override void Enter()
        {
            base.Enter();
            Destroy();
            AddExcuteAfterAniListener(DeadEffect);
        }
    }
}
