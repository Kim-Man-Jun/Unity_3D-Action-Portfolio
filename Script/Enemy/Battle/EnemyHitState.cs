using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(EnemyController _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.zeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.zeroVelocity();

        if (triggerCalled)
        {
            float attackDisToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

            if (attackDisToPlayer > 3f)
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
            else if (attackDisToPlayer < 3f)
            {
                enemy.transform.LookAt(enemy.player.transform.position);
                stateMachine.ChangeState(enemy.attackState);
            }
        }
    }
}
