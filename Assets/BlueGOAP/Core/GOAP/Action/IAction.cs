namespace BlueGOAP
{
    public interface IAction<TAction>
    {
        /// <summary>
        /// 当前动作的标签
        /// </summary>
        TAction Label { get; }
        /// <summary>
        /// 动作花费 默认为1
        /// </summary>
        int Cost { get; }
        /// <summary>
        /// 动作执行的优先级 默认为0
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// 当前动作是否能够中断
        /// </summary>
        bool CanInterruptiblePlan { get; }
        /// <summary>
        /// 执行动作的先决条件
        /// </summary>
        IState Preconditions { get; }

        /// <summary>
        /// 动作执行后的状态
        /// </summary>
        IState Effects { get; }
        /// <summary>
        /// 验证先决条件
        /// </summary>
        /// <returns></returns>
        bool VerifyPreconditions();
    }
}