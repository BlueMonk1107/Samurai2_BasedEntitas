using System.Collections.Generic;

namespace BlueGOAP
{
    public interface IFsmState<TLabel>
    {
        /// <summary>
        /// 动作执行状态
        /// </summary>
        ActionExcuteState ExcuteState { get; }
        /// <summary>
        /// 动作状态标签
        /// </summary>
        TLabel Label { get; }

        /// <summary>
        /// 进入动作
        /// </summary>
        void Enter();
        /// <summary>
        /// 更新动作
        /// </summary>
        void Execute();
        /// <summary>
        /// 退出动作
        /// </summary>
        void Exit();
    }

    public interface IFSM<TLabel>
    {
        /// <summary>
        /// 添加需要管理的状态
        /// </summary>
        /// <param name="label"></param>
        /// <param name="state"></param>
        void AddState(TLabel label, IFsmState<TLabel> state);
        /// <summary>
        /// 执行新状态
        /// </summary>
        /// <param name="newState"></param>
        void ExcuteNewState(TLabel newState);
        /// <summary>
        /// 帧函数
        /// </summary>
        void FrameFun();
    }
    /// <summary>
    /// 动作执行状态枚举
    /// </summary>
    public enum ActionExcuteState
    {
        INIT,
        ENTER,
        EXCUTE,
        EXIT
    }

    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="TLabel"></typeparam>
    public class ActionFSM<TLabel> : IFSM<TLabel>
    {
        private readonly Dictionary<TLabel, IFsmState<TLabel>> _stateDic;
        private IFsmState<TLabel> _currentState;
        private IFsmState<TLabel> _previousState;

        public ActionFSM()
        {
            _stateDic = new Dictionary<TLabel, IFsmState<TLabel>>();
        }

        //注册一个新状态到字典里
        public void AddState(TLabel label, IFsmState<TLabel> state)
        {
            _stateDic[label] = state;
        }

        //执行新状态
        public void ExcuteNewState(TLabel newState)
        {
            if (!_stateDic.ContainsKey(newState))
            {
                DebugMsg.LogError("状态机内不包含此状态对象:" + newState);
                return;
            }
            _previousState = _currentState;
            _currentState = _stateDic[newState];

            if (_previousState != null)
                _previousState.Exit();

            if (_currentState != null)
                _currentState.Enter();
        }
        
        //执行状态，每帧执行
        public void FrameFun()
        {
            if (_currentState != null)
            {
                _currentState.Execute();
            }
        }
    }

    public class ActionStateFSM<TLabel> : IFSM<TLabel>
    {
        private Dictionary<TLabel, IFsmState<TLabel>> _stateDic;
        public ActionStateFSM()
        {
            _stateDic = new Dictionary<TLabel, IFsmState<TLabel>>();
        }

        public void AddState(TLabel label, IFsmState<TLabel> state)
        {
            _stateDic.Add(label, state);
        }

        public void ExcuteNewState(TLabel newState)
        {
            if (!_stateDic.ContainsKey(newState))
            {
                DebugMsg.LogError("状态机内不包含此状态对象:" + newState);
                return;
            }

            IFsmState<TLabel> state = _stateDic[newState];
            state.Enter();
        }

        public void FrameFun()
        {
            foreach (var handler in _stateDic)
            {
                if (handler.Value.ExcuteState == ActionExcuteState.ENTER || handler.Value.ExcuteState == ActionExcuteState.EXCUTE)
                {
                    handler.Value.Execute();
                }

            }
        }
    }
}
