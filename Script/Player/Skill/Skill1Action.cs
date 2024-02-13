using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Action : MonoBehaviour
{
    EnemyController enemy;
    skillManager skillManager;

    public ParticleSystem posionParticle;

    public int posionDamage = 10;
    public float posionInterval = 1;
    public float posionDuration = 5;

    Coroutine posionCoroutine;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        skillManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<skillManager>();

        SFXManager.instance.Skill1Sound();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject posionEffect = skillManager.GetSkillPool(skillManager.skill1Effect, skillManager.skill1EffectPool);
            posionEffect.transform.position = other.transform.position;
            posionEffect.SetActive(true);

            StartCoroutine(ReturnSkill1EffectPool(posionEffect));

            posionCoroutine = StartCoroutine(posionDamageOn(other));

            StartCoroutine(stopPosionCoroutine());
        }
    }

    IEnumerator ReturnSkill1EffectPool(GameObject posion)
    {
        yield return new WaitForSeconds(5f);
        skillManager.ReturnSkillPool(posion, skillManager.skill1EffectPool);
    }

    IEnumerator posionDamageOn(GameObject enemy)
    {
        while (true)
        {
            enemy.GetComponent<EnemyController>().Damaged(posionDamage);

            yield return new WaitForSeconds(posionInterval);
        }
    }

    IEnumerator stopPosionCoroutine()
    {
        yield return new WaitForSeconds(posionDuration);

        if (posionCoroutine != null)
        {
            StopCoroutine(posionCoroutine);
        }
    }
}
