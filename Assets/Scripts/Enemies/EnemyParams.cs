using UnityEngine;
using System;

public class EnemyParams : MonoBehaviour
{
    public float Health = 100;
    public int KillCost = 10;
    public GameObject ImpactEffect;
    public Spown[] Spowns;

    bool dead = false;
    EnemyMovenment movenment;

    private void Start()
    {
        GameManager.EnemiesLeft++;
        movenment = GetComponent<EnemyMovenment>();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        // if (ImpactEffect != null) Instantiate(ImpactEffect, transform.position, transform.rotation);

        if (Health < 0 && !dead)
        {
            dead = true;

            if (movenment != null) movenment.isActive = false;
            
            FindObjectOfType<PlayerParams>().AddMoney(KillCost);
            // enemies left -- in void OnDestroy
            if (Spowns.Length > 0) foreach (var item in Spowns)
                    Instantiate(item.EnemyPrefab, item.SpownPoint.position, item.SpownPoint.rotation);

            Animator anim = GetComponent<Animator>();
            if (anim != null) anim.SetTrigger("destroy");

            Destroy(gameObject, 1f);
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameManager>().ChangeEnemiesLeft(); // enemies left --
    }
}

[Serializable]
public class Spown
{
    public GameObject EnemyPrefab;
    public Transform SpownPoint;
}
