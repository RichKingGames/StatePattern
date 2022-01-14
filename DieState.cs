using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    private void BeforeDieAction()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(BeforeDieAction);
        agent.OnAgentDie?.Invoke();
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.die);
        agent.animationManager.OnAnimationEnd.AddListener(BeforeDieAction);
        agent.rb.velocity = new Vector2(0, agent.rb.velocity.y);
    }

    public override void UpdateState()
    {
        agent.rb.velocity = new Vector2(0, agent.rb.velocity.y);
    }
 
    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }

}
