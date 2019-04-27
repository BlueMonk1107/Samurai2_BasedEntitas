
using System;
using System.Collections.Generic;

namespace BlueGOAP
{
    /// <summary>
    /// 动作等对象的映射管理类
    /// </summary>
    public interface IMaps<TAction, TGoal>
    {
        IActionHandler<TAction> GetActionHandler(TAction actionLabel);
        IGoal<TGoal> GetGoal(TGoal goalLabel);
        void SetGameData<Tkey>(Tkey key,object value);
        TClass GetGameData<Tkey, TClass>(Tkey key) where Tkey : struct where TClass : class;
        TValue GetGameDataValue<Tkey, TValue>(Tkey key) where Tkey : struct where TValue : struct;
        object GetGameData<Tkey>(Tkey key);
    }

    public abstract class MapsBase<TAction, TGoal> : IMaps<TAction, TGoal>
    {
        private Dictionary<TAction, IActionHandler<TAction>> _actionHandlerDic;
        private Dictionary<TGoal, IGoal<TGoal>> _goalsDic;
        protected IAgent<TAction, TGoal> _agent;
        private Dictionary<string, object> _gameData;
        private ObjectPool _pool;

        public MapsBase(IAgent<TAction, TGoal> agent, 
            Action<IAgent<TAction, TGoal>, IMaps<TAction, TGoal>> onInitGameData)
        {
            _agent = agent;
            _pool = ObjectPool.Instance;
            _actionHandlerDic = new Dictionary<TAction, IActionHandler<TAction>>();
            _goalsDic = new Dictionary<TGoal, IGoal<TGoal>>();
            _gameData = new Dictionary<string, object>();
            InitGameData(onInitGameData);
            InitActinMaps();
            InitGoalMaps();
        }

        /// <summary>
        /// 在此函数内手动填写对应的动作数据
        /// </summary>
        protected abstract void InitActinMaps();
        /// <summary>
        /// 在此函数内手动填写对应的目标数据
        /// </summary>
        protected abstract void InitGoalMaps();

        /// <summary>
        /// 初始化游戏内数据
        /// </summary>
        protected virtual void InitGameData(Action<IAgent<TAction, TGoal>, IMaps<TAction, TGoal>> onInitGameData)
        {
            if (onInitGameData != null)
                onInitGameData(_agent,this);
        }

        /// <summary>
        /// 获取动作数据
        /// </summary>
        /// <param name="actionLabel"></param>
        /// <returns></returns>
        public IActionHandler<TAction> GetActionHandler(TAction actionLabel)
        {
            IActionHandler<TAction> handler;
            _actionHandlerDic.TryGetValue(actionLabel, out handler);
            if(handler == null)
                DebugMsg.LogError("action:"+ actionLabel+" not init");
            return handler;
        }
        /// <summary>
        /// 获取目标数据
        /// </summary>
        /// <param name="goalLabel"></param>
        /// <returns></returns>
        public IGoal<TGoal> GetGoal(TGoal goalLabel)
        {
            IGoal<TGoal> goal;
            _goalsDic.TryGetValue(goalLabel, out goal);
            if (goal == null)
                DebugMsg.LogError("goal:" + goalLabel + " not init");
            return goal;
        }

        public void SetGameData<Tkey>(Tkey key, object value)
        {
            _gameData[key.ToString()] = value;
        }

        public TClass GetGameData<Tkey, TClass>(Tkey key) where Tkey : struct where TClass : class
        {
            TClass c = GetGameData(key) as TClass;
            if (c == null)
            {
                DebugMsg.LogError("无法转换类型");
            }

            return c;
        }

        public TValue GetGameDataValue<Tkey, TValue>(Tkey key) where Tkey : struct where TValue : struct
        {
            try
            {
                return (TValue)GetGameData(key);
            }
            catch (Exception)
            {
                DebugMsg.LogError("无法转换类型");
                return default(TValue);
            }
        }

        public object GetGameData<Tkey>(Tkey key)
        {
            if (_gameData.ContainsKey(key.ToString()))
            {
                return _gameData[key.ToString()];
            }
            else
            {
                DebugMsg.LogError("can not find key name is " + key);
                return null;
            }
        }

        protected void AddAction<THandler, UAction>()
         where THandler : class, IActionHandler<TAction>
         where UAction : class, IAction<TAction>
        {
            UAction action = _pool.Spwan<UAction>(_agent);
            THandler handler = _pool.Spwan<THandler>(_agent, this, action);

            if (!_actionHandlerDic.ContainsKey(handler.Label))
            {
                _actionHandlerDic.Add(handler.Label, handler);
            }
            else
            {
                DebugMsg.LogError("发现具有重复标签的Handler，标签为：" + handler.Label);
            }
        }

        protected void AddGoal<UGoal>()
             where UGoal : class, IGoal<TGoal>
        {
            UGoal goal = _pool.Spwan<UGoal>(_agent);
            if (!_goalsDic.ContainsKey(goal.Label))
            {
                _goalsDic.Add(goal.Label, goal);
            }
            else
            {
                DebugMsg.LogError("发现具有相同目标的Goal，标签为：" + goal.Label);
            }
        }
    }
}
