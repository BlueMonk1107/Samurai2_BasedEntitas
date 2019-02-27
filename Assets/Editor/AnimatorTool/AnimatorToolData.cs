using UnityEngine;

namespace CustomTool
{
    [System.Serializable]
    public class AnimatorToolData : ScriptableObject
    {
        [SerializeField]
        public string AnimatorControllerPath;
    }
}
