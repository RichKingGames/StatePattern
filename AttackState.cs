using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : JumpState
{
    [SerializeField]
    public LayerMask hittableLayerMask;

    protected Vector2 direction;

    protected override void EnterState()
    {
        agent.animationManager.ResetEvents();
        agent.animationManager.PlayAnimation(AnimationType.attack);
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdleState);
        agent.animationManager.OnAnimationAction.AddListener(PerformAttack);

        agent.agentWeapon.ToggleWeaponVisibility(true);
        direction = agent.transform.right * (agent.transform.localScale.x>0 ? 1 :-1);
        if (agent.groundDetector.isGrounded)
            agent.rb.velocity = Vector2.zero;
        
    }

    public override void UpdateState()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
    }

    private void TransitionToIdleState()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdleState);
        if (agent.groundDetector.isGrounded)
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        else
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));     
    }

    private void PerformAttack()
    {
        agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);
        agent.agentWeapon.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, direction);
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

    protected override void ExitState(){}
}
