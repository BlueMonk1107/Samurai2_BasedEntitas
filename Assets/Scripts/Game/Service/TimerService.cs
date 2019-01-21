using Entitas;
using UnityEngine;

namespace Game.Service
{
    public interface ITimerService : IInitService, IExecuteService
    {
        
    }

    public class TimerService : ITimerService
    {
        public void Init(Contexts contexts)
        {
            contexts.game.SetGameTimerService(this);
        }

        public TimerService()
        {
            
        }

        public void Excute()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
