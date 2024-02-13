using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill2State : PlayerState
{
    public PlayerSkill2State(playerController player, PlayerStateMachine stateMachine, string animBoolName) 
        : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        //파이어볼 생성
        player.Skill2Instantiate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
