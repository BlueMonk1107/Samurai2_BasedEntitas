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

        public IdleSwordHandler(IAgent<ActionEnum, GoalEnum> agent, IAction<ActionEnum> action) : base(agent, action)
        {
            IsNeedResetPreconditions = false;
            _timerService = Contexts.sharedInstance.service.gameServiceTimerService.TimerService;
        }

        public override void Enter()
        {
            base.Enter();
            DebugMsg.Log("进入攻击待机状态");
            _timer = _timerService.CreatOrRestartTimer("IdleSwordHandler", Const.IDLE_SWORD_DELAY_TIME, false);
            _timer.AddCompleteListener(OnComplete);
        }

        public override void Exit()
        {
            base.Exit();
            _timerService.StopTimer(_timer,false);
        }
    }
}
