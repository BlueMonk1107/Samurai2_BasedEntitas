using System.Collections;
using System.Collections.Generic;
using Const;
using Game.Service;
using Module.Timer;
using UnityEngine;

public class IdleSwordAniState : StateMachineBehaviour
{
    private ITimerService _timerService;
    private ITimer _timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Init();
        _timer = _timerService.CreateTimer("IdleSwordAniState", 1, false);
        _timer.AddCompleteListener(() => animator.SetBool(ConstValue.IDLE_SWORD_PARA_NAME, false));
    }

    private void Init()
    {
        if (_timerService == null)
        {
            _timerService = Contexts.sharedInstance.service.gameServiceTimerService.TimerService;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timerService.StopTimer(_timer, false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
