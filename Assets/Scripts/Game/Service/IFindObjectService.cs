using UnityEngine;

namespace Game
{
    /// <summary>
    /// 查找场景内物体服务接口
    /// </summary>
    public interface IFindObjectService
    {
        T[] FindAllType<T>() where T : Object;
        IView[] FindAllView();
    }
}
