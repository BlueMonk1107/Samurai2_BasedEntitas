using UnityEngine;

namespace Game
{
    public class InputFeature : Feature
    {
        public InputFeature(Contexts contexts) : base("System")
        {
            InitializeFun(contexts);
            ExecuteFun(contexts);
            CleanupFun(contexts);
            TearDownFun(contexts);
            ReactiveSystemFun(contexts);
        }

        private void InitializeFun(Contexts contexts)
        {
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
