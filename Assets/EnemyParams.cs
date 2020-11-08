using UnityEngine;

public class EnemyParams : MonoBehaviour
{
    public float Health = 100;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            GetComponent<EnemyMovenment>().isActive = false;
            FindObjectOfType<GameManager>().ChangeEnemiesLeft(); // enemies left --
        }
    }
}
