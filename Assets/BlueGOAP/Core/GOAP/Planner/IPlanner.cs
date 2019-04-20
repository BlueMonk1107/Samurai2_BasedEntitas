using System.Collections.Generic;

namespace BlueGOAP
{
    public interface IPlanner<TAction, TGoal>
    {
        /// <summary>
        /// 计划
        /// </summary>
        Queue<IActionHandler<TAction>> BuildPlan(IGoal<TGoal> goal);
    }
}
