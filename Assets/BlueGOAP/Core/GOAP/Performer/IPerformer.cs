
namespace BlueGOAP
{
    public interface IPerformer
    {
        /// <summary>
        /// 更新数据函数
        /// </summary>
        void UpdateData();
        /// <summary>
        /// 中断计划
        /// </summary>
        void Interruptible();
    }
}
