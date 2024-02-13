using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Action : MonoBehaviour
{
    EnemyController enemy;
    skillManager skillManager;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        skillManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<skillManager>();

        SFXManager.instance.Skill2Sound();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.Damaged(3);

            GameObject skill2Effect = skillManager.GetSkillPool(skillManager.skill2Effect, skillManager.skill2EffectPool);
            Vector3 spawnPosition = transform.position + transform.forward * 0.5f;
            Quaternion spawnRotation = transform.rotation;
            skill2Effect.transform.position = spawnPosition;
            skill2Effect.transform.rotation = spawnRotation;
            skill2Effect.SetActive(true);

            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[10];

            int collisionNum = particleSystem.GetCollisionEvents(other, collisionEvents);

            for (int i = 0; i < collisionNum; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;

                SFXManager.instance.Skill2Hit();
            }

            Invoke("ReturnSkill2EffectPool", 0.5f);
        }
    }

    void ReturnSkill2EffectPool()
    {
        GameObject skill2Effect = GameObject.FindGameObjectWithTag("Skill2Effect");
        skillManager.ReturnSkillPool(skill2Effect, skillManager.skill2EffectPool);
    }
}
