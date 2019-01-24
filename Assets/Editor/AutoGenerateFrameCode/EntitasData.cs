using UnityEngine;

namespace CustomTool
{
    [System.Serializable]
    public class EntitasData : ScriptableObject     
    {
        /// <summary>
        /// View层路径
        /// </summary>
        public string ViewPath;
        /// <summary>
        /// Service层路径
        /// </summary>
        public string ServicePath;
        /// <summary>
        /// System层路径
        /// </summary>
        public string SystemPath;
        /// <summary>
        /// ServiceManager路径
        /// </summary>
        public string ServiceManagerPath;
    }
}
