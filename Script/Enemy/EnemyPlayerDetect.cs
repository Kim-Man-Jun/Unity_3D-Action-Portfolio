using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetect : MonoBehaviour
{
    EnemyController enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.playerEncount = true;
        }
    }
}
