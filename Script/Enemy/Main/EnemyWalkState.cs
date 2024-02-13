using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    protected EnemyController enemy;

    private float RandomX;
    private float RandomZ;

    public EnemyWalkState(EnemyController _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemyBase;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.movingTime;

        RandomX = Random.Range(-1, 2);
        RandomZ = Random.Range(-1, 2);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(RandomX * enemy.moveSpeed, RandomZ * enemy.moveSpeed, enemy.rotateSpeed);
        enemy.transform.LookAt(enemy.transform);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        float attackDisToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        if (attackDisToPlayer < 3f)
        {
            enemy.transform.LookAt(enemy.player.transform.position);
            stateMachine.ChangeState(enemy.attackState);
        }
    }
}
