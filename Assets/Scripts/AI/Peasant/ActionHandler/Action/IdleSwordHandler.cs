using System;
using System.Threading.Tasks;
using BlueGOAP;
using Game.AI.ViewEffect;
using Game.Service;
using Module.Timer;
using UnityEngine;

namespace Game.AI
{
    public class IdleSwordHandler : HandlerBase<IModel>
    {
        public IdleSwordHandler(IAgent<ActionEnum, GoalEnum> agent,
            IMaps<ActionEnum, GoalEnum> maps, 
            IAction<ActionEnum> action) 
            : base(agent,maps, action)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入攻击待机状态");
            CreateTimer(Const.IDLE_SWORD_DELAY_TIME);
        }
    }
}
