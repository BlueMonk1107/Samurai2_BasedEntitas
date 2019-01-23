using Entitas;
using UnityEngine;

namespace Game
{
    public class Test : MonoBehaviour,IInitializeSystem,IExecuteSystem,ICleanupSystem,ITearDownSystem
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }

        public void TearDown()
        {
            throw new System.NotImplementedException();
        }
    }
}
