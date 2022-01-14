using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    protected bool jumpPressed = false;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.jump);
        movementData.currentVelocity = agent.rb.velocity;
        movementData.currentVelocity.y = agent.agentData.jumpForce;
        agent.rb.velocity = movementData.currentVelocity;
        jumpPressed = true;
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    protected override void HandleAttack()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
    }

    protected void ControlJumpHeight()
    {
        if(jumpPressed == false)
        {
            movementData.currentVelocity = agent.rb.velocity;
            movementData.currentVelocity.y += agent.agentData.lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime;
            agent.rb.velocity = movementData.currentVelocity;
        }
    }

    public override void UpdateState()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
        if (agent.rb.velocity.y <= 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        }
    }
}
