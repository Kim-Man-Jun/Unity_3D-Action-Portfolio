using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private float idleTime;

    public PlayerIdleState(playerController player, PlayerStateMachine stateMachine, string animBoolName) 
        : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        idleTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.xInput != 0 || player.zInput != 0)
        {
            if (player.runOnOff == true)
            {
                player.stateMachine.ChangeState(player.runState);
            }
            else
            {
                player.stateMachine.ChangeState(player.walkState);
            }
        }
    }
}
