using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int Damage = 20, Speed = 100;
    [Range(1, 10)]
    public int CollisionCount = 1;
    public float TimerToDestroy = 3.5f;

    private void Start() { Destroy(gameObject, TimerToDestroy + Random.Range(0f, .5f)); }

    private void Update()
    {
        transform.Translate(transform.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyParams>();
        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
            if (enemy.ImpactEffect != null) Instantiate(enemy.ImpactEffect, transform.position, transform.rotation);
            CollisionCount--;
            if (CollisionCount <= 0) Destroy(gameObject);
        }
    }
}
