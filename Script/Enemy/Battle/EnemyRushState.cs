using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRushState : EnemyState
{
    protected EnemyController enemy;

    float rushReadyTime = 4;

    Transform playerPos;

    public EnemyRushState(EnemyController _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemyBase;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.zeroVelocity();

        rushReadyTime = 4;

        enemy.playerEncount = false;
        playerPos = enemy.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.transform.LookAt(playerPos);

        rushReadyTime -= Time.deltaTime;

        if (rushReadyTime < 0)
        {
            //방향 계산
            Vector3 direction = (playerPos.position - enemy.transform.position).normalized;
            enemy.rbody.MovePosition(enemy.transform.position + direction * enemy.rushSpeed * Time.deltaTime);

            //플레이어와 거리 계산
            float distToPlayer = Vector3.Distance(enemy.transform.position, playerPos.position);
            float attackDisToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

            if (distToPlayer < 3f)
            {
                stateMachine.ChangeState(enemy.idleState);

                if (attackDisToPlayer < 3f)
                {
                    enemy.transform.LookAt(enemy.player.transform.position);
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
    }
}
