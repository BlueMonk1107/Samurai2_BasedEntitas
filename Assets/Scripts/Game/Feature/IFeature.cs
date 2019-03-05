using UnityEngine;

namespace Game
{
    public interface IFeature
    {
        void InitializeFun(Contexts contexts);

        void ExecuteFun(Contexts contexts);

        void CleanupFun(Contexts contexts);

        void TearDownFun(Contexts contexts);

        void ReactiveSystemFun(Contexts contexts);
    }
}
