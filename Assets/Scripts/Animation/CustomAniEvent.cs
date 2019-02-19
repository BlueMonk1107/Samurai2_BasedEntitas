using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

/// <summary>
/// 自定义动画事件
/// </summary>
public class CustomAniEvent : StateMachineBehaviour {
    
    public Action<string> OnStateEnterAction { get; set; }
    public Action<string> OnStateUpdateAction { get; set; }
    public Action<string> OnStateExitAction { get; set; }

    public void Init(PlayerAniStateName stateName)
    {
        name = stateName.ToString();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnStateEnterAction?.Invoke(name);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnStateUpdateAction?.Invoke(name);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnStateExitAction?.Invoke(name);
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
