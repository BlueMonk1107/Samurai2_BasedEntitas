using BlueGOAP;
using Game.AI.ViewEffect;
using Game.Service;
using Module.Timer;
using UnityEngine;

namespace Game.AI
{
    public class EnterAlertHandler : HandlerBase<EnterAlertModel>
    {
        public EnterAlertHandler(IAgent<ActionEnum, GoalEnum> agent, 
            IMaps<ActionEnum, GoalEnum> maps, 
            IAction<ActionEnum> action) 
            : base(agent, maps, action)
        {
        }
    }
}
