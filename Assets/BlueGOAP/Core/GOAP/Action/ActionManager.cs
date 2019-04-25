
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueGOAP
{
    public abstract class ActionManagerBase<TAction, TGoal> : IActionManager<TAction>
    {
        private Dictionary<TAction, IActionHandler<TAction>> _handlerDic;
        private Dictionary<TAction, IActionHandler<TAction>> _mutilActionHandlers;
        /// <summary>
        /// 能够打断计划的动作
        /// </summary>
        private List<IActionHandler<TAction>> _interruptibleHandlers;
        private IFSM<TAction> _fsm;
        private IFSM<TAction> _mutilFsm;
        private IAgent<TAction, TGoal> _agent;
        public bool IsPerformAction { get; set; }
        //效果的键值和动作的映射关系
        public Dictionary<string, HashSet<IActionHandler<TAction>>> EffectsAndActionMap { get; private set; }
        private Action<TAction> _onActionComplete;

        public ActionManagerBase(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
            _handlerDic = new Dictionary<TAction, IActionHandler<TAction>>();
            _mutilActionHandlers = new Dictionary<TAction, IActionHandler<TAction>>();
            _interruptibleHandlers = new List<IActionHandler<TAction>>();
            _fsm = new FSM<TAction>();
            InitActionHandlers();
            InitMutilActionHandlers();
            _mutilFsm = new MutilActionFSM<TAction>();
            InitFsm();
            InitMutilFSM();
            InitEffectsAndActionMap();
            InitInterruptibleDic();
        }

        /// <summary>
        /// 初始化当前代理的动作处理器
        /// </summary>
        protected abstract void InitActionHandlers();
        /// <summary>
        /// 初始化当前可叠加执行动作处理器
        /// </summary>
        protected abstract void InitMutilActionHandlers();
        /// <summary>
        /// 初始化动作和动作影响的映射
        /// </summary>
        private void InitEffectsAndActionMap()
        {
            EffectsAndActionMap = new Dictionary<string, HashSet<IActionHandler<TAction>>>();

            foreach (var handler in _handlerDic)
            {
                IState state = handler.Value.Action.Effects;

                if(state == null)
                    continue;

                foreach (string key in state.GetKeys())
                {
                    if (!EffectsAndActionMap.ContainsKey(key) || EffectsAndActionMap[key] == null)
                        EffectsAndActionMap[key] = new HashSet<IActionHandler<TAction>>();

                    EffectsAndActionMap[key].Add(handler.Value);
                }
            }
        }
        /// <summary>
        /// 初始化能够打断计划的动作缓存
        /// </summary>
        private void InitInterruptibleDic()
        {
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> handler in _handlerDic)
            {
                if (handler.Value.Action.CanInterruptiblePlan)
                {
                    _interruptibleHandlers.Add(handler.Value);
                }
            }
            //按照优先级排序
            _interruptibleHandlers = _interruptibleHandlers.OrderByDescending(u => u.Action.Priority).ToList();
        }

        public abstract TAction GetDefaultActionLabel();

        public void AddHandler(TAction label)
        {
            AddHandler(label, _handlerDic);
        }

        public void AddMutilActionHandler(TAction label)
        {
            AddHandler(label, _mutilActionHandlers);
        }

        private void AddHandler(TAction label, Dictionary<TAction, IActionHandler<TAction>> dic)
        {
            var handler = _agent.Maps.GetActionHandler(label);
            if (handler != null)
            {
                dic.Add(label, handler);
                //这里写拉姆达表达式，是为了避免初始化的时候_onActionComplete还是null的
                handler.AddFinishCallBack(() =>
                {
                    DebugMsg.Log("动作完成：   "+label);
                    _onActionComplete(label);
                });
            }
            else
            {
                DebugMsg.LogError("映射文件中未找到对应Handler,标签为:" + label);
            }
        }

        public void RemoveHandler(TAction actionLabel)
        {
            _handlerDic.Remove(actionLabel);
        }

        public IActionHandler<TAction> GetHandler(TAction actionLabel)
        {
            if (_handlerDic.ContainsKey(actionLabel))
            {
                return _handlerDic[actionLabel];
            }

            DebugMsg.LogError("Not add action name:" + actionLabel);
            return null;
        }

        public void FrameFun()
        {
            if (IsPerformAction)
                _fsm.FrameFun();

            _mutilFsm.FrameFun();
        }

        public void UpdateData()
        {
            JudgeInterruptibleHandler();
            JudgeConformMutilAction();
        }
        //判断是否有能够打断计划的动作执行
        private void JudgeInterruptibleHandler()
        {
            foreach (var handler in _interruptibleHandlers)
            {
                if (handler.CanPerformAction())
                {
                    DebugMsg.LogError(handler.Label + "打断计划");
                    _agent.Performer.Interruptible();
                    break;
                }
            }
        }
        //判断是否有满足条件的可叠加动作
        private void JudgeConformMutilAction()
        {
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> pair in _mutilActionHandlers)
            {
                if (_agent.AgentState.ContainState(pair.Value.Action.Preconditions))
                {
                    if (pair.Value.ExcuteState == ActionExcuteState.INIT || pair.Value.ExcuteState == ActionExcuteState.EXIT)
                        ExcuteNewState(pair.Key);
                }
            }
        }

        public virtual void ExcuteNewState(TAction label)
        {
            if (_handlerDic.ContainsKey(label))
            {
                _fsm.ExcuteNewState(label);
            }
            else if (_mutilActionHandlers.ContainsKey(label))
            {
                _mutilFsm.ExcuteNewState(label);
            }
            else
            {
                DebugMsg.LogError("动作" + label + "不在当前动作缓存内");
            }
        }

        public void AddActionCompleteListener(Action<TAction> actionComplete)
        {
            _onActionComplete = actionComplete;
        }

        private void InitFsm()
        {
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> handler in _handlerDic)
            {
                _fsm.AddState(handler.Key, handler.Value);
            }
        }

        private void InitMutilFSM()
        {
            foreach (var handler in _mutilActionHandlers)
            {
                _mutilFsm.AddState(handler.Key, handler.Value);
            }
        }
    }
}
