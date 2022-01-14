using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.idle);
        if (agent.groundDetector.isGrounded)
            agent.rb.velocity = Vector2.zero;
    }

    public override void UpdateState()
    {
        if (TestFallTransition())
            return;
        if(Mathf.Abs(agent.agentInput.MovementVector.x) > 0f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Move));
        }
    }
}
