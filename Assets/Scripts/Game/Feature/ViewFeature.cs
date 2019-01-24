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
            InitializeFun(contexts);
            ExecuteFun(contexts);
            CleanupFun(contexts);
            TearDownFun(contexts);
            ReactiveSystemFun(contexts);
        }

        private void InitializeFun(Contexts contexts)
        {
            Add(new InitViewSystem(contexts));
        }

        private void ExecuteFun(Contexts contexts)
        {
        }

        private void CleanupFun(Contexts contexts)
        {
        }

        private void TearDownFun(Contexts contexts)
        {
        }

        private void ReactiveSystemFun(Contexts contexts)
        {

        }
    }
}
