using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class ViewBase<T> : IFsmState<T>
    {
        public ActionExcuteState ExcuteState { get; protected set; }
        public abstract T Label { get; }
        public abstract string AniName { get; }

        protected IModel _iModel;
        protected EffectMgr _effectMgr;
        protected AIAniMgr _AniMgr;
        protected AIVIewEffectMgrBase<T> _mgr;

        public ViewBase(AIVIewEffectMgrBase<T> mgr)
        {
            _mgr = mgr;
            _iModel = InitModel(mgr);
             _effectMgr = mgr.EffectMgr;
            _AniMgr = mgr.AniMgr;
        }

        private IModel InitModel(AIVIewEffectMgrBase<T> mgr)
        {
            IModel model = mgr.ModelMgr.GetModel<IModel>(Label); 
            if (model != null)
            {
                model.AniDutation = mgr.AniMgr.GetAniLength(AniName);
            }
            return model;
        }

        public virtual void Enter()
        {
            ExcuteState = ActionExcuteState.ENTER;
            _AniMgr.Play(AniName);
        }

        public virtual void Execute()
        {
            ExcuteState = ActionExcuteState.EXCUTE;
        }

        public virtual void Exit()
        {
            ExcuteState = ActionExcuteState.EXIT;
        }
    }
}
