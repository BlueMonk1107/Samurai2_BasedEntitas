using UnityEngine;

namespace Game
{
    public class SystemFeature : Feature
    {
        public SystemFeature(Contexts contexts) : base("System")
        {
            AddInputSystem(contexts);
        }

        private void AddInputSystem(Contexts contexts)
        {
            Add(new InputUpButtonSystem(contexts));
            Add(new InputDownButtonSystem(contexts));
            Add(new InputLeftButtonSystem(contexts));
            Add(new InputRightButtonSystem(contexts));
            Add(new InputAttackOButtonSystem(contexts));
            Add(new InputAttackXButtonSystem(contexts));
        }
    }
}
