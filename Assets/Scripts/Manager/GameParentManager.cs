using System.Collections.Generic;
using UnityEngine;

namespace Manager.Parent
{
    /// <summary>
    /// 场景内父物体管理类
    /// </summary>
    public class GameParentManager : MonoBehaviour
    {
        private Dictionary<string, Transform> _parentDic;
        public void Init()
        {
            _parentDic = new Dictionary<string, Transform>();
            foreach (Transform trans in transform)
            {
                _parentDic[trans.name] = trans;
            }
        }

        /// <summary>
        /// 通过父物体名称获取父物体对象
        /// </summary>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public Transform GetParnetTrans(ParentName parentName)
        {
            Transform parnet = null;
            _parentDic.TryGetValue(parentName.ToString(), out parnet);
            return parnet;
        }

        /// <summary>
        /// 通过父物体名称获取父物体对象
        /// </summary>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public Transform GetParnetTrans(string parentName)
        {
            Transform parnet = null;
            _parentDic.TryGetValue(parentName, out parnet);
            return parnet;
        }
    }

    public enum ParentName
    {
        PlayerRoot,
        CameraController,
        UIController
    }
}
