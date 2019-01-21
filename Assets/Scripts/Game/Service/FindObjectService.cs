using System.Linq;
using Game.Service;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 查找场景内物体的服务
    /// </summary>
    public class FindObjectService : IFindObjectService
    {
        public void Init(Contexts contexts)
        {
            contexts.game.SetGameFindObjectService(this);
        }

        public T[] FindAllType<T>() where T : Object
        {
            T[] temp = Object.FindObjectsOfType<T>();
            if (temp == null || temp.Length == 0)
            {
                Debug.LogError("未找到类型：" + typeof(T).FullName + "对象");
            }
            return temp;
        }

        /// <summary>
        /// 查找场景内的View
        /// </summary>
        /// <returns></returns>
        public IView[] FindAllView()
        {
            return FindAllType<ViewService>();
        }
       
    }
}
