
namespace BlueGOAP
{
    public class Performer<TAction, TGoal> : IPerformer
    {
        private IPlanHandler<TAction> _planHandler;
        private IPlanner<TAction, TGoal> _planner;
        private IGoalManager<TGoal> _goalManager;
        private IActionManager<TAction> _actionManager;

        public Performer(IAgent<TAction, TGoal> agent)
        {
            _planHandler = new PlanHandler<TAction>();
            _planHandler.AddCompleteCallBack(PlanComplete);
            _planner = new Planner<TAction, TGoal>(agent);
            _goalManager = agent.GoalManager;
            _actionManager = agent.ActionManager;
            _actionManager.AddActionCompleteListener(PlanActionComplete);
        }

        public void UpdateData()
        {
            if (WhetherToReplan())
            {
                DebugMsg.Log("制定新计划");
                BuildPlanAndStart();
            }
        }

        public void Interruptible()
        {
            DebugMsg.Log("打断计划");
            _planHandler.Interruptible();
        }

        //制定计划并开始计划
        private void BuildPlanAndStart()
        {
            if(_goalManager.CurrentGoal != null)
                DebugMsg.Log("----------------新的目标：" + _goalManager.CurrentGoal.Label.ToString());
            //若目标完成则重新寻找目标
            var plan = _planner.BuildPlan(_goalManager.CurrentGoal);
            if (plan != null && plan.Count > 0)
            {
                _planHandler.Init(_actionManager, plan);
                _planHandler.StartPlan();
                _actionManager.IsPerformAction = true;
            }
        }

        //计划完成
        private void PlanComplete()
        {
            DebugMsg.Log("计划完成");
            _actionManager.IsPerformAction = false;
        }

        //计划完成了当前动作
        private void PlanActionComplete()
        {
            DebugMsg.Log("下一步");
            _planHandler.NextAction();
        }

        //检测是否需要重新制定计划
        private bool WhetherToReplan()
        {
            //当前计划是否完成
            return _planHandler.IsComplete;
        }
    }
}
