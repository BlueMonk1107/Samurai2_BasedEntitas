using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace CustomTool
{
    [System.Serializable]
    public class AnimatorToolData : ScriptableObject
    {
        [SerializeField]
        public string AnimatorControllerPath;
        /// <summary>
        /// 添加了Help的状态机
        /// </summary>
        [SerializeField]
        public List<AnimatorController> HelpControllers;
    }
}
