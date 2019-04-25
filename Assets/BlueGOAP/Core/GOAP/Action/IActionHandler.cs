using System;
using UnityEngine;

namespace BlueGOAP
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    public interface IActionHandler<TAction> : IFsmState<TAction>
    {
        /// <summary>
        /// 动作
        /// </summary>
        IAction<TAction> Action { get; }
        /// <summary>
        /// 判断当前状态是否能够执行动作
        /// </summary>
        /// <returns></returns>
        bool CanPerformAction();
        /// <summary>
        /// 添加动作完成回调
        /// </summary>
        /// <param name="onFinishAction"></param>
        void AddFinishCallBack(System.Action onFinishAction);
    }
}
