using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDmg : MonoBehaviour
{
    playerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Damaged(1);
        }
    }
}
