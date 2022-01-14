using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MovementState
{
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.fall);
    }

    public override void UpdateState()
    {
        movementData.currentVelocity = agent.rb.velocity;
        movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;
        agent.rb.velocity = movementData.currentVelocity;

        CalculateVelocity();
        SetPlayerVelocity();
        if(agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected override void HandleAttack()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
    }
}
