
using System.Collections.Generic;
using System.Linq;

namespace BlueGOAP
{
    public abstract class GoalManagerBase<TAction, TGoal> : IGoalManager<TGoal>
    {
        private Dictionary<TGoal, IGoal<TGoal>> _goalsDic;
        private IAgent<TAction, TGoal> _agent;
        public IGoal<TGoal> CurrentGoal { get; private set; }
        private List<IGoal<TGoal>> _activeGoals;

        public GoalManagerBase(IAgent<TAction, TGoal> agent)
        {
            _agent = agent;
            CurrentGoal = null;
            _goalsDic = new Dictionary<TGoal, IGoal<TGoal>>();
            _activeGoals = new List<IGoal<TGoal>>();
            InitGoals();
        }

        /// <summary>
        /// 初始化当前代理的目标
        /// </summary>
        protected abstract void InitGoals();

        public void AddGoal(TGoal goalLabel)
        {
            var goal = _agent.Maps.GetGoal(goalLabel);
            if (goal != null)
            {
                _goalsDic.Add(goalLabel, goal);
                goal.AddGoalActivateListener((activeGoal) =>
                {
                    if (!_activeGoals.Contains(activeGoal))
                    {
                        _activeGoals.Add(activeGoal);
                    }
                });
                goal.AddGoalInactivateListener((activeGoal) =>
                {
                    if (_activeGoals.Contains(activeGoal))
                    {
                        _activeGoals.Remove(activeGoal);
                    }
                });
            }
        }

        public void RemoveGoal(TGoal goalLabel)
        {
            _goalsDic.Remove(goalLabel);
        }

        public IGoal<TGoal> GetGoal(TGoal goalLabel)
        {
            if (_goalsDic.ContainsKey(goalLabel))
            {
                return _goalsDic[goalLabel];
            }

            DebugMsg.LogError("Not add goal name:" + goalLabel);
            return null;
        }

        public IGoal<TGoal> FindGoal()
        {
            //查找优先级最大的那个
            _activeGoals = _activeGoals.OrderByDescending(u => u.GetPriority()).ToList();

            DebugMsg.Log("-----------active goal-----------");
            foreach (IGoal<TGoal> goal in _activeGoals)
            {
                DebugMsg.Log(goal.Label + " 优先级：" + goal.GetPriority());
            }
            DebugMsg.Log("---------------------------------");

            if (_activeGoals.Count > 0)
            {
                return _activeGoals[0];
            }
            else
            {
                return null;
            }
        }

        public void UpdateData()
        {
            DebugMsg.Log("GoalManager UpdateData");
            UpdateGoals();
            UpdateCurrentGoal();
        }
        //更新所有Goal的信息
        private void UpdateGoals()
        {
            foreach (KeyValuePair<TGoal, IGoal<TGoal>> goal in _goalsDic)
            {
                goal.Value.UpdateData();
            }
        }
        //更新CurrentGoal对象
        private void UpdateCurrentGoal()
        {
            CurrentGoal = FindGoal();
            if (CurrentGoal != null)
                DebugMsg.Log("CurrentGoal:" + CurrentGoal.Label.ToString());
            else
                DebugMsg.Log("CurrentGoal is null");
        }
    }
}
