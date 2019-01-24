using UnityEngine;

namespace Game
{
    public class InputFeature : Feature
    {
        public InputFeature(Contexts contexts) : base("System")
        {
            AddInputSystem(contexts);
        }

        private void AddInputSystem(Contexts contexts)
        {
            Add(new InputNullSysytem(contexts));
            Add(new InputForwardButtonSystem(contexts));
            Add(new InputBackButtonSystem(contexts));
            Add(new InputLeftButtonSystem(contexts));
            Add(new InputRightButtonSystem(contexts));
            Add(new InputAttackOButtonSystem(contexts));
            Add(new InputAttackXButtonSystem(contexts));
            Add(new InputMoveButtonSystem(contexts));
        }
    }
}
