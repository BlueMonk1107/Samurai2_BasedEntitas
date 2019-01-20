using Entitas;
using UnityEngine;

namespace Game
{
    public interface ITimerService
    {
        
    }

    public class TimerService : IExecuteSystem, ITimerService
    {
        public TimerService(Contexts contexts)
        {
            
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
