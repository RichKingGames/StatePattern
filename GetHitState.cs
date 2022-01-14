using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : JumpState
{
    private void TransitionToIdle()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdle);
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.hit);
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdle);
    }

    protected override void HandleAttack()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdle);
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
    }

    protected override void HandleJumpPressed()
    {
        movementData.currentVelocity = agent.rb.velocity;
        movementData.currentVelocity.y = agent.agentData.jumpForce;
        agent.rb.velocity = movementData.currentVelocity;
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    public override void UpdateState()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
    }
}
