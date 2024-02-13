using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillManager : MonoBehaviour
{
    playerController player;
    EnemyController enemy;

    [Header("Skill Object Pool")]
    public GameObject SkillObjectPool;

    public GameObject attackObj;
    public GameObject attackEffect;
    public GameObject skill1Obj;
    public GameObject skill1Effect;
    public GameObject skill2Obj;
    public GameObject skill2Effect;
    public GameObject skill3Obj;

    public int poolSize = 20;

    [Header("Skill Ojbect Pools")]
    public List<GameObject> attackPool = new List<GameObject>();
    public List<GameObject> attackEffectPool = new List<GameObject>();
    public List<GameObject> skill1Pool = new List<GameObject>();
    public List<GameObject> skill1EffectPool = new List<GameObject>();
    public List<GameObject> skill2Pool = new List<GameObject>();
    public List<GameObject> skill2EffectPool = new List<GameObject>();
    public List<GameObject> skill3Pool = new List<GameObject>();

    [Header("Skill CoolTime")]
    public bool skill1CooltimeOn = false;
    public float skill1Cooltime = 10;
    public Image skill1CooltimeDelay;

    public bool skill2CooltimeOn = false;
    public float skill2Cooltime = 8;
    public Image skill2CooltimeDelay;

    public bool skill3CooltimeOn = false;
    public float skill3Cooltime = 14;
    public Image skill3CooltimeDelay;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();

        InitializePool(attackObj, attackPool);
        InitializePool(attackEffect, attackEffectPool);
        InitializePool(skill1Obj, skill1Pool);
        InitializePool(skill1Effect, skill1EffectPool);
        InitializePool(skill2Obj, skill2Pool);
        InitializePool(skill2Effect, skill2EffectPool);
        InitializePool(skill3Obj, skill3Pool);
    }

    void InitializePool(GameObject obj, List<GameObject> pool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject skill = Instantiate(obj, transform.position, Quaternion.identity, SkillObjectPool.transform);
            skill.SetActive(false);
            pool.Add(skill);
        }
    }

    public GameObject GetSkillPool(GameObject obj, List<GameObject> pool)
    {
        foreach (GameObject skill in pool)
        {
            if (!skill.activeInHierarchy)
            {
                return skill;
            }
        }

        // Pool에 재사용 가능한 스킬 오브젝트가 없는 경우 추가로 생성
        GameObject newSkill = Instantiate(obj, transform.position, Quaternion.identity);
        newSkill.SetActive(false);
        pool.Add(newSkill);

        return newSkill;
    }

    public void ReturnSkillPool(GameObject obj, List<GameObject> pool)
    {
        obj.SetActive(false);
    }

    private void Update()
    {
        if (skill1CooltimeOn == true)
        {
            skill1Cooltime -= Time.deltaTime;

            if (skill1Cooltime <= 0)
            {
                skill1CooltimeDelay.fillAmount = 0;
                skill1CooltimeOn = false;
            }
            else
            {
                skill1CooltimeDelay.fillAmount = skill1Cooltime / 10;
                player.skill1Posion = false;
            }
        }

        if (skill2CooltimeOn == true)
        {
            skill2Cooltime -= Time.deltaTime;

            if (skill2Cooltime <= 0)
            {
                skill2CooltimeDelay.fillAmount = 0;
                skill2CooltimeOn = false;
            }
            else
            {
                skill2CooltimeDelay.fillAmount = skill2Cooltime / 8;
                player.skill2Fire = false;
            }
        }

        if (skill3CooltimeOn == true)
        {
            skill3Cooltime -= Time.deltaTime;

            if (skill3Cooltime <= 0)
            {
                skill3CooltimeDelay.fillAmount = 0;
                skill3CooltimeOn = false;
            }
            else
            {
                skill3CooltimeDelay.fillAmount = skill3Cooltime / 14;
                player.skill3Heal = false;
            }
        }
    }

    #region Button Action
    public void attackOn()
    {
        player.attackOn = true;
    }

    public void posionSkill()
    {
        if (player.skill1Posion == false && skill1CooltimeOn == false)
        {
            player.skill1Posion = true;

            skill1CooltimeOn = true;
            skill1Cooltime = 10;
        }
    }

    public void fireSkill()
    {
        if (player.skill2Fire == false && skill2CooltimeOn == false)
        {
            player.skill2Fire = true;

            skill2CooltimeOn = true;
            skill2Cooltime = 8;
        }
    }

    public void healSkill()
    {
        if (player.skill3Heal == false && skill3CooltimeOn == false)
        {
            player.skill3Heal = true;

            skill3CooltimeOn = true;
            skill3Cooltime = 14;
        }
    }

    public void walkRunToggle()
    {
        player.runOnOff = !player.runOnOff;
    }
    #endregion
}
