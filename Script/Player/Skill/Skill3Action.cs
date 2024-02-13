using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Action : MonoBehaviour
{
    playerController player;

    public ParticleSystem healParticle;

    public int healEffect = 1;
    public float healInterval = 1;
    public float healDuration = 4;

    Coroutine healCoroutine;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();

        SFXManager.instance.Skill3Sound();

        healCoroutine = StartCoroutine(playerHeal());

        StartCoroutine(stopHealCoroutine());
    }

    IEnumerator playerHeal()
    {
        while (true)
        {
            if (player.playerNowHp < player.playerMaxHp)
            {
                player.playerNowHp++;
            }

            yield return new WaitForSeconds(healInterval);
        }
    }

    IEnumerator stopHealCoroutine()
    {
        yield return new WaitForSeconds(healDuration);

        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
        }
    }
}
