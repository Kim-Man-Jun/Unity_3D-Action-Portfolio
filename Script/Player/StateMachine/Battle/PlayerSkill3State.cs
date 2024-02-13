using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill3State : PlayerState
{
    public PlayerSkill3State(playerController player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        //�÷��̾� �ֺ��� �� ����
        player.Skill3Instantiate();
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
