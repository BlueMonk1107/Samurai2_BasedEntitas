using System;
using System.Threading.Tasks;
using BlueGOAP;
using Game.Service;
using Module.Timer;
using UnityEngine;

namespace Game.AI
{
    public class IdleSwordHandler : ActionHandlerBase<ActionEnum, GoalEnum>
    {
        private ITimerService _timerService;
        private ITimer _timer;
        private static int _times;
        private int _id;

        public IdleSwordHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
            _timerService = Contexts.sharedInstance.service.gameServiceTimerService.TimerService;
            _id = _times++;
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入攻击待机状态");
            _timer = _timerService.CreatOrRestartTimer("IdleSwordHandler"+ _id, Const.IDLE_SWORD_DELAY_TIME, false);
            _timer.AddCompleteListener(OnComplete);
        }

        public override void Exit()
        {
            base.Exit();
            _timerService.StopTimer(_timer,false);
        }
    }
}
