using System.Collections.Generic;
using UnityEngine;

namespace CustomTool
{
    [System.Serializable]
    public class SubAnimatorMachineItem     
    {
        [SerializeField]
        public string SubMachineName;
        [SerializeField]
        public List<GameObject> AnimationObjects = new List<GameObject>();
        /// <summary>
        /// 是否允许右键快速添加动画文件
        /// </summary>
        public bool IsAutoAdd;
    }
}
