using UnityEngine;

namespace Game.Service
{
    /// <summary>
    /// 初始化服务接口
    /// </summary>
    public interface IInitService
    {
        void Init(Contexts contexts);
    }

    /// <summary>
    /// 帧函数接口
    /// </summary>
    public interface IExecuteService
    {
        void Excute();
    }
}
