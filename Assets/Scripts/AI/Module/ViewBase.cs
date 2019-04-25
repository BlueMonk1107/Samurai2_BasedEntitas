using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class ViewBase<T> : IFsmState<T>
    {
        public ActionExcuteState ExcuteState { get; private set; }
        public abstract T Label { get; }

        protected IModel _iModel;
        protected EffectMgr _effectMgr;

        public ViewBase(AIVIewEffectMgrBase<T> mgr)
        {
            _iModel = mgr.ModelMgr.GetModel(Label);
            _effectMgr = mgr.EffectMgr;
        }

        public virtual void Enter()
        {
            ExcuteState = ActionExcuteState.ENTER;
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
