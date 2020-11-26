using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int Damage = 20, Speed = 100;
    public float TimerToDestroy = 3.5f;

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
            Destroy(gameObject);
        }
    }
}
