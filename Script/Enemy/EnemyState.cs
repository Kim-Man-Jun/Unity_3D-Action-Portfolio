using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected EnemyController enemy;
    protected Rigidbody rbody;

    protected bool triggerCalled;
    //Idle 상태 중 대기하는 시간
    protected float stateTimer;

    private string animBoolName;

    public EnemyState(EnemyController _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rbody = enemy.rbody;
        enemy.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}