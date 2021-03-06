using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField]
    protected MovementData movementData;

    private void Awake()
    {
        movementData = GetComponentInParent<MovementData>();
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run);

        movementData.horizontalMovementDirection = 0;
        movementData.currentSpeed = agent.agentData.maxSpeed;
        movementData.currentVelocity = Vector2.zero;
    }

    protected void SetPlayerVelocity()
    {
        agent.rb.velocity = movementData.currentVelocity;
    }

    protected void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementVector, movementData);
        CalculateHorizontalDirection(movementData);
        movementData.currentVelocity = Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;
        movementData.currentVelocity.y = agent.rb.velocity.y;
    }

    protected void CalculateHorizontalDirection(MovementData movementData)
    {
        if(agent.agentInput.MovementVector.x > 0)
            movementData.horizontalMovementDirection = 1;

        else if (agent.agentInput.MovementVector.x < 0)
            movementData.horizontalMovementDirection = -1;
    }

    protected void CalculateSpeed(Vector2 movementVector, MovementData movementData)
    {
        if(Mathf.Abs(movementVector.x) > 0)
        {
            movementData.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
        }
        else 
        {
            movementData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;
        }
        movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, agent.agentData.maxSpeed); 
    }

    public override void UpdateState()
    {
        if (TestFallTransition())
            return;
        CalculateVelocity();
        SetPlayerVelocity();

        if (Mathf.Abs(agent.rb.velocity.x) < 0.01f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }
}
