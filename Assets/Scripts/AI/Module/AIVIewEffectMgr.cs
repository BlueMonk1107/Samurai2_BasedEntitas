using System.Collections.Generic;
using BlueGOAP;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class AIVIewEffectMgrBase<T>
    {
        private IFSM<T> _fsm;
        private IFSM<T> _mutilFsm;
        private Dictionary<T, IFsmState<T>> _viewDic;
        private Dictionary<T, IFsmState<T>> _mutilActonsView;
        public AIModelMgrBase<T> ModelMgr { get; private set; }
        public EffectMgr EffectMgr { get; private set; }
        public AudioMgr AudioMgr { get; private set; }
        public AIAniMgr AniMgr { get; private set; }

        public AIVIewEffectMgrBase(string enemyID, object source, object ani)
        {
            _fsm = new ActionFSM<T>();
            _mutilFsm = new ActionStateFSM<T>();
            _viewDic = new Dictionary<T, IFsmState<T>>();
            _mutilActonsView = new Dictionary<T, IFsmState<T>>();
            EffectMgr = new EffectMgr();
            AudioMgr = new AudioMgr(enemyID, source);
            AniMgr = new AIAniMgr(ani);

            ModelMgr = InitModelMgr();

            InitViews();
            InitFsm();

            InitMutilViews();
            InitMutilFsm();
        }

        private void InitFsm()
        {
            foreach (KeyValuePair<T, IFsmState<T>> state in _viewDic)
            {
                _fsm.AddState(state.Key, state.Value);
            }
        }

        private void InitMutilFsm()
        {
            foreach (KeyValuePair<T, IFsmState<T>> state in _mutilActonsView)
            {
                _mutilFsm.AddState(state.Key, state.Value);
            }
        }

        protected abstract void InitViews();
        protected abstract void InitMutilViews();
        protected abstract AIModelMgrBase<T> InitModelMgr();

        protected void AddView(IFsmState<T> state)
        {
            T key = state.Label;
            if (_viewDic.ContainsKey(key))
            {
                DebugMsg.LogError("已包含当前键值");
            }
            else
            {
                _viewDic.Add(key,state);
            }
        }

        protected void AddMutilView(IFsmState<T> state)
        {
            T key = state.Label;
            if (_mutilActonsView.ContainsKey(key))
            {
                DebugMsg.LogError("已包含当前键值");
            }
            else
            {
                _mutilActonsView.Add(key, state);
            }
        }

        public void ExcuteState(T key)
        {
            if (_viewDic.ContainsKey(key))
            {
                _fsm.ExcuteNewState(key);
            }
            else if (_mutilActonsView.ContainsKey(key))
            {
                _mutilFsm.ExcuteNewState(key);
            }
            else
            {
                DebugMsg.LogError("动作" + key + "不在当前动作缓存内");
            }
        }
    }

    public class AIVIewEffectMgr : AIVIewEffectMgrBase<ActionEnum>
    {

        public AIVIewEffectMgr(string enemyID,object source,object ani) : base(enemyID,source, ani)
        {
            
        }

        protected override void InitViews()
        {
            AddView(new AttackView(this));
            AddView(new DeadView(this));
            AddView(new IdleSwordView(this));
            AddView(new IdleView(this));
            AddView(new InjureView(this));
            AddView(new MoveBackwardView(this));
            AddView(new MoveView(this));
        }

        protected override void InitMutilViews()
        {
            AddMutilView(new AlertView(this));
        }

        protected override AIModelMgrBase<ActionEnum> InitModelMgr()
        {
            return new AIModelMgr();
        }
    }
}
