
using System;
using System.Collections.Generic;

namespace BlueGOAP
{
    /// <summary>
    /// 事件管理
    /// </summary>
    public interface IActionManager<TAction>
    {
        /// <summary>
        /// 是否执行动作
        /// </summary>
        bool IsPerformAction { get; set; }
        /// <summary>
        /// 效果和动作的映射关系
        /// </summary>
        Dictionary<string, HashSet<IActionHandler<TAction>>> EffectsAndActionMap { get; }
        /// <summary>
        /// 获取默认动作的标签
        /// </summary>
        /// <returns></returns>
        TAction GetDefaultActionLabel();
        /// <summary>
        /// 添加处理类对象
        /// </summary>
        void AddHandler(TAction actionLabel);
        /// <summary>
        /// 移除处理类对象
        /// </summary>
        /// <param name="handler"></param>
        void RemoveHandler(TAction actionLabel);
        /// <summary>
        /// 获取处理类对象
        /// </summary>
        /// <param name="handler"></param>
        IActionHandler<TAction> GetHandler(TAction actionLabel);
        /// <summary>
        /// 帧函数
        /// </summary>
        void FrameFun();
        /// <summary>
        /// 更新数据
        /// </summary>
        void UpdateData();
        /// <summary>
        /// 改变当前执行的动作
        /// </summary>
        /// <param name="actionLabel"></param>
        void ExcuteNewState(TAction actionLabel);
        /// <summary>
        /// 添加动作完成的监听
        /// </summary>
        /// <param name="actionComplete"></param>
        void AddActionCompleteListener(Action<TAction> actionComplete);
    }
}
