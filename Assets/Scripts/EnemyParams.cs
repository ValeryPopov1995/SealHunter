﻿using UnityEngine;

public class EnemyParams : MonoBehaviour
{
    public float Health = 100;
    public int KillCost = 10;

    bool dead = false;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0 && !dead)
        {
            dead = true;

            GetComponent<EnemyMovenment>().isActive = false;
            FindObjectOfType<GameManager>().ChangeEnemiesLeft(); // enemies left --
            FindObjectOfType<PlayerParams>().Money += KillCost;
            Destroy(gameObject, 1f);
        }
    }
}
