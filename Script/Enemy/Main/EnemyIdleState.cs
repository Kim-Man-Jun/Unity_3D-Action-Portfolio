using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected EnemyController enemy;

    public EnemyIdleState(EnemyController _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemyBase;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.zeroVelocity();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.walkState);
        }

        float attackDisToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        if (attackDisToPlayer < 3f)
        {
            enemy.transform.LookAt(enemy.player.transform.position);
            stateMachine.ChangeState(enemy.attackState);
        }
    }
}
