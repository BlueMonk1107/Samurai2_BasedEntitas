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
        protected AIAniMgr _aniMgr;
        protected AiViewMgrBase<T> _mgr;
        protected AudioMgr _AudioMgr;

        public ViewBase(AiViewMgrBase<T> mgr)
        {
            _mgr = mgr;
            _iModel = InitModel(mgr);
             _effectMgr = mgr.EffectMgr;
            _aniMgr = mgr.AniMgr;
            _AudioMgr = mgr.AudioMgr;
        }

        private IModel InitModel(AiViewMgrBase<T> mgr)
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
            _aniMgr.Play(AniName);
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
