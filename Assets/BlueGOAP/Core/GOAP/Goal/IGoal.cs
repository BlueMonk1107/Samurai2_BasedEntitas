using System;
using System.Collections;

namespace BlueGOAP
{
    public interface IGoal<TGoal>
    {
        /// <summary>
        /// 目标的标签
        /// </summary>
        TGoal Label { get; }
        /// <summary>
        /// 获取优先级
        /// </summary>
        /// <returns></returns>
        float GetPriority();
        /// <summary>
        /// 获取目标对状态的影响
        /// </summary>
        /// <returns></returns>
        IState GetEffects();
        /// <summary>
        /// 是否已经实现目标
        /// </summary>
        /// <returns></returns>
        bool IsGoalComplete();
        /// <summary>
        /// 添加目标激活的监听
        /// </summary>
        /// <param name="onActivate"></param>
        void AddGoalActivateListener(Action<IGoal<TGoal>> onActivate);
        /// <summary>
        /// 添加目标未激活的监听
        /// </summary>
        /// <param name="onInactivate"></param>
        void AddGoalInactivateListener(Action<IGoal<TGoal>> onInactivate);
        void UpdateData();
    }
}
