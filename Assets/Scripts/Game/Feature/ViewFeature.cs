using UnityEngine;

namespace Game
{
    /// <summary>
    /// VIew层系统部分
    /// </summary>
    public class ViewFeature : Feature
    {
        public ViewFeature(Contexts contexts) : base("View")
        {
            Init(contexts);
        }

        private void Init(Contexts contexts)
        {
            Add(new InitViewSystem(contexts));
        }
    }
}
