using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bullet : MonoBehaviour
{
    public int Damage = 20, Speed = 50, TimerToDestroy = 5;
    public GameObject ImpactEffect;

    private void Start() { Destroy(gameObject, TimerToDestroy); }

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
            if (ImpactEffect != null) Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
