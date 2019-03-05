using UnityEngine;

namespace Game
{
    public class InputFeature : Feature,IFeature
    {
        public InputFeature(Contexts contexts) : base("System")
        {
            InitializeFun(contexts);
            ExecuteFun(contexts);
            CleanupFun(contexts);
            TearDownFun(contexts);
            ReactiveSystemFun(contexts);
        }

        public void InitializeFun(Contexts contexts)
        {
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
            Add(new InputHumanSkillStateSystem(contexts));
            Add(new InputJudgeHumanSkillSystem(contexts));
            Add(new InputNullSysytem(contexts));
            Add(new InputForwardButtonSystem(contexts));
            Add(new InputBackButtonSystem(contexts));
            Add(new InputLeftButtonSystem(contexts));
            Add(new InputRightButtonSystem(contexts));
            Add(new InputMoveButtonSystem(contexts));
        }
    }
}
