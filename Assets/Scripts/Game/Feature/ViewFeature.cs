using UnityEngine;

namespace Game
{
    /// <summary>
    /// VIew层系统部分
    /// </summary>
    public class ViewFeature : Feature, IFeature
    {
        public ViewFeature(Contexts contexts) : base("View")
        {
            InitializeFun(contexts);
            ExecuteFun(contexts);
            CleanupFun(contexts);
            TearDownFun(contexts);
            ReactiveSystemFun(contexts);
        }

        public void InitializeFun(Contexts contexts)
        {
            Add(new InitViewSystem(contexts));
        }

        public void ExecuteFun(Contexts contexts)
        {
        }

        public void CleanupFun(Contexts contexts)
        {
        }

        public void TearDownFun(Contexts contexts)
        {
        }

        public void ReactiveSystemFun(Contexts contexts)
        {

        }
    }
}
