using BlueGOAP;
using Game.Service;
using Module.Timer;
using UnityEngine;

namespace Game.AI.ViewEffect
{
    public abstract class HandlerBase<TModel> : ActionHandlerBase<ActionEnum, GoalEnum> where TModel : class ,IModel
    {
        protected ITimerService _timerService;
        protected ITimer _timer;
        protected TModel _model;

        public HandlerBase(
            IAgent<ActionEnum, GoalEnum> agent, 
            IMaps<ActionEnum, GoalEnum> maps,
            IAction<ActionEnum> action) 
            : base(agent, maps, action)
        {
            _timer = null;
            _model = this.GetModel<TModel>(_maps);
            _timerService = Contexts.sharedInstance.service.gameServiceTimerService.TimerService;
        }

        public override void Enter()
        {
            base.Enter();
            if (_model != null && _model.AniDutation > 0)
            {
                CreateTimer(_model.AniDutation);
            }
        }

        protected void CreateTimer(float time,bool loop = false)
        {
            _timer = _timerService.CreatOrRestartTimer(Label.ToString() + ID, time, loop);
            _timer.AddCompleteListener(() => OnComplete());
        }

        public override void Exit()
        {
            base.Exit();
            if (_timer != null)
            {
                _timerService.StopTimer(_timer, false);
            }
        }
    }
}
