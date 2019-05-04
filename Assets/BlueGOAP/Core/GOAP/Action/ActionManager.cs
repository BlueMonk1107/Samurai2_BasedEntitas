
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueGOAP
{
    public abstract class ActionManagerBase<TAction, TGoal> : IActionManager<TAction>
    {
        /// <summary>
        /// 动作字典
        /// </summary>
        private Dictionary<TAction, IActionHandler<TAction>> _actionHandlerDic;
        /// <summary>
        /// 动作状态字典
        /// </summary>
        private Dictionary<TAction, IActionHandler<TAction>> _actionStateHandlers;
        /// <summary>
        /// 能够打断计划的动作
        /// </summary>
        private List<IActionHandler<TAction>> _interruptibleHandlers;
        /// <summary>
        /// 动作 状态机
        /// </summary>
        private IFSM<TAction> _actionFsm;
        /// <summary>
        /// 动作状态 状态机
        /// </summary>
        private IFSM<TAction> _actionStateFsm;
        private IAgent<TAction, TGoal> _agent;
        /// <summary>
        /// 当前是否在执行动的标志位（用于避免动作已经结束，却还在执行帧函数的情况）
        /// </summary>
        public bool IsPerformAction { get; set; }
        /// <summary>
        /// 效果的键值和动作的映射关系
        /// </summary>
        public Dictionary<string, HashSet<IActionHandler<TAction>>> EffectsAndActionMap { get; private set; }
        //动作完成的回调
        private Action<TAction> _onActionComplete;

        public ActionManagerBase(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
            _actionHandlerDic = new Dictionary<TAction, IActionHandler<TAction>>();
            _actionStateHandlers = new Dictionary<TAction, IActionHandler<TAction>>();
            _interruptibleHandlers = new List<IActionHandler<TAction>>();
            _actionFsm = new ActionFSM<TAction>();
            _actionStateFsm = new ActionStateFSM<TAction>();

            InitActionHandlers();
            InitActionStateHandlers();
           
            InitFsm();
            InitActionStateFSM();

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
        protected abstract void InitActionStateHandlers();
        /// <summary>
        /// 初始化动作和动作影响的映射
        /// </summary>
        private void InitEffectsAndActionMap()
        {
            EffectsAndActionMap = new Dictionary<string, HashSet<IActionHandler<TAction>>>();

            foreach (var handler in _actionHandlerDic)
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
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> handler in _actionHandlerDic)
            {
                if (handler.Value.Action.CanInterruptiblePlan)
                {
                    _interruptibleHandlers.Add(handler.Value);
                    DebugMsg.LogError(handler.Value.Action.Priority.ToString());
                }
            }
            //按照优先级排序
            _interruptibleHandlers = _interruptibleHandlers.OrderByDescending(u => u.Action.Priority).ToList();
        }

        public abstract TAction GetDefaultActionLabel();

        public void AddHandler(TAction label)
        {
            AddHandler(label, _actionHandlerDic);
        }

        public void AddMutilActionHandler(TAction label)
        {
            AddHandler(label, _actionStateHandlers);
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
            _actionHandlerDic.Remove(actionLabel);
        }

        public IActionHandler<TAction> GetHandler(TAction actionLabel)
        {
            if (_actionHandlerDic.ContainsKey(actionLabel))
            {
                return _actionHandlerDic[actionLabel];
            }

            DebugMsg.LogError("Not add action name:" + actionLabel);
            return null;
        }

        public void FrameFun()
        {
            if (IsPerformAction)
                _actionFsm.FrameFun();

            _actionStateFsm.FrameFun();
        }

        public void UpdateData()
        {
            JudgeInterruptibleHandler();
            JudgeConformActionState();
        }
        //判断是否有能够打断计划的动作执行
        private void JudgeInterruptibleHandler()
        {
            foreach (var handler in _interruptibleHandlers)
            {
                if (handler.CanPerformAction())
                {
                    DebugMsg.Log(handler.Label + "打断计划");
                    _agent.Performer.Interruptible();
                    break;
                }
            }
        }
        //判断是否有满足条件的可叠加动作
        private void JudgeConformActionState()
        {
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> pair in _actionStateHandlers)
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
            if (_actionHandlerDic.ContainsKey(label))
            {
                _actionFsm.ExcuteNewState(label);
            }
            else if (_actionStateHandlers.ContainsKey(label))
            {
                _actionStateFsm.ExcuteNewState(label);
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
            foreach (KeyValuePair<TAction, IActionHandler<TAction>> handler in _actionHandlerDic)
            {
                _actionFsm.AddState(handler.Key, handler.Value);
            }
        }

        private void InitActionStateFSM()
        {
            foreach (var handler in _actionStateHandlers)
            {
                _actionStateFsm.AddState(handler.Key, handler.Value);
            }
        }
    }
}
