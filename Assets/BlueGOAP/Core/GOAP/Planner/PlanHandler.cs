using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueGOAP
{
    public class PlanHandler<TAction> : IPlanHandler<TAction>
    {
        private Queue<IActionHandler<TAction>> _plan;
        private IActionManager<TAction> _actionManager;
        private Action _onComplete;
        private IActionHandler<TAction> _currentActionHandler;
        private bool _isInterruptible;

        public bool IsComplete {
            get
            {
                if (_isInterruptible)
                    return true;

                if (_plan == null)
                {
                    return true;
                }

                if (_currentActionHandler == null)
                {
                    return _plan.Count == 0;
                }
                else
                {
                    return _currentActionHandler.ExcuteState == ActionExcuteState.EXIT && _plan.Count == 0;
                }
            }
        }

        public void Init(IActionManager<TAction> actionManager, Queue<IActionHandler<TAction>> plan)
        {
            _isInterruptible = false;
            _currentActionHandler = null;
            _actionManager = actionManager;
            _plan = plan;
        }

        public void AddCompleteCallBack(Action onComplete)
        {
            _onComplete = onComplete;
        }

        public void StartPlan()
        {
            NextAction();
        }

        public void NextAction()
        {
            if (IsComplete)
            {
                _onComplete();
            }
            else
            {
                _currentActionHandler = _plan.Dequeue();
                DebugMsg.Log("----当前执行动作:"+ _currentActionHandler.Label);
                _actionManager.ExcuteNewState(_currentActionHandler.Label);
            }
        }

        public void Interruptible()
        {
            _isInterruptible = true;
        }
    }
}
