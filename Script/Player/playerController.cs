using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : basicMovement
{
    [Header("Player Move")]
    public float xInput;
    public float zInput;
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;
    public bool runOnOff = false;

    [Header("Player Battle")]
    public int playerMaxHp;
    public int playerNowHp;

    public bool attackOn;
    public float attackPower;

    [Header("Player Skill")]
    public bool skill1Posion;
    public bool skill2Fire;
    public bool skill3Heal;

    skillManager skillManager;

    [Header("Player Camera")]
    public GameObject cam;

    EnemyController enemy;

    #region stateMachine
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerRunState runState { get; private set; }

    public PlayerAttackState attackState { get; private set; }
    public PlayerSkill1State skill1State { get; private set; }
    public PlayerSkill2State skill2State { get; private set; }
    public PlayerSkill3State skill3State { get; private set; }

    public PlayerHitState hitState { get; private set; }
    public PlayerVictoryState victoryState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkState = new PlayerWalkState(this, stateMachine, "Walk");
        runState = new PlayerRunState(this, stateMachine, "Run");

        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        skill1State = new PlayerSkill1State(this, stateMachine, "Skill1");
        skill2State = new PlayerSkill2State(this, stateMachine, "Skill2");
        skill3State = new PlayerSkill3State(this, stateMachine, "Skill3");

        hitState = new PlayerHitState(this, stateMachine, "Hit");
        victoryState = new PlayerVictoryState(this, stateMachine, "Victory");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        skillManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<skillManager>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (attackOn == true)
        {
            stateMachine.ChangeState(attackState);
        }

        if (skill1Posion == true && skillManager.skill1CooltimeOn == true)
        {
            stateMachine.ChangeState(skill1State);
        }

        if (skill2Fire == true && skillManager.skill2CooltimeOn == true)
        {
            stateMachine.ChangeState(skill2State);
        }

        if (skill3Heal == true && skillManager.skill3CooltimeOn == true)
        {
            stateMachine.ChangeState(skill3State);
        }

        if (enemy.GetComponent<EnemyController>().enemyNowHp <= 0)
        {
            stateMachine.ChangeState(victoryState);
        }
    }

    //basicMovement »ó¼Ó¿ë
    public void zeroVelocity()
    {
        ZeroVelocity();
    }

    public void animationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    public void Damaged(int damage)
    {
        playerNowHp--;

        if (playerNowHp >= 1)
        {
            SFXManager.instance.PlayerHit();
            stateMachine.ChangeState(hitState);
        }

        else if (playerNowHp < 1)
        {
            SFXManager.instance.PlayerHit();

            BGMManager.instance.musicStop();
            BGMManager.instance.DefeatSound();

            StartCoroutine(playerDead());
        }
    }

    IEnumerator playerDead()
    {
        stateMachine.ChangeState(deadState);

        yield return new WaitForSeconds(1.4f);
    }

    public void AttackEffectInstantiate()
    {
        GameObject attack = skillManager.GetSkillPool(skillManager.attackObj, skillManager.attackPool);
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0.5f);
        Quaternion spawnRotation = transform.rotation;
        attack.transform.position = spawnPosition;
        attack.transform.rotation = spawnRotation;
        attack.SetActive(true);

        Invoke("ReturnAttackPool", 0.5f);
    }

    void ReturnAttackPool()
    {
        GameObject attack = GameObject.FindGameObjectWithTag("Attack");
        skillManager.ReturnSkillPool(attack, skillManager.attackPool);
    }

    public void Skill1Instantiate()
    {
        GameObject skill1 = skillManager.GetSkillPool(skillManager.skill1Obj, skillManager.skill1Pool);
        Vector3 spawnPosition = transform.position + transform.forward * 3.0f;
        Quaternion spawnRotation = transform.rotation;
        skill1.transform.position = spawnPosition;
        skill1.transform.rotation = spawnRotation;
        skill1.SetActive(true);

        Invoke("ReturnSkill1Pool", 5f);
    }

    void ReturnSkill1Pool()
    {
        GameObject skill = GameObject.FindGameObjectWithTag("Skill1");
        skillManager.ReturnSkillPool(skill, skillManager.skill1Pool);
    }


    public void Skill2Instantiate()
    {
        GameObject skill2 = skillManager.GetSkillPool(skillManager.skill2Obj, skillManager.skill2Pool);
        Vector3 spawnPosition = transform.position + transform.forward * 1.0f;
        Quaternion spawnRotation = transform.rotation;
        skill2.transform.position = spawnPosition;
        skill2.transform.rotation = spawnRotation;
        skill2.SetActive(true);

        Invoke("ReturnSkill2Pool", 3f);
    }

    void ReturnSkill2Pool()
    {
        GameObject skill2 = GameObject.FindGameObjectWithTag("Skill2");
        skillManager.ReturnSkillPool(skill2, skillManager.skill2Pool);
    }

    public void Skill3Instantiate()
    {
        GameObject skill3 = skillManager.GetSkillPool(skillManager.skill3Obj, skillManager.skill3Pool);
        Vector3 spawnPosition = transform.position + transform.forward * -0.05f;
        Quaternion spawnRotation = transform.rotation;
        skill3.transform.position = spawnPosition;
        skill3.transform.rotation = spawnRotation;
        skill3.SetActive(true);

        Invoke("ReturnSkill3Pool", 2f);

        //Vector3 spawnPosition = transform.position + transform.forward * -0.05f;
        //Quaternion spawnRotation = transform.rotation;

        //GameObject heal = Instantiate(skill3Obj, spawnPosition, spawnRotation);

        //Destroy(heal, 2f);
    }

    void ReturnSkill3Pool()
    {
        GameObject skill3 = GameObject.FindGameObjectWithTag("Skill3");
        skillManager.ReturnSkillPool(skill3, skillManager.skill3Pool);
    }
}
