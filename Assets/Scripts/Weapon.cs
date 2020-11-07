using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage = 10, Rate = 100;
    public int BulletsMax = 10, BulletsCurrent = 10;
    public ParticleSystem ShootParticles;

    bool readyToShoot = true;

    public IEnumerator Shoot()
    {
        if (readyToShoot)
        {
            readyToShoot = false;

            // magic
            if (ShootParticles != null) ShootParticles.Play();
            // shoot
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
            {
                // impact
                // Instantiate(ImpactEffect, hit.transform.position, hit.transform.rotation);

                // damage
                EnemyParams enemy = hit.transform.GetComponent<EnemyParams>();
                if (enemy != null) enemy.TakeDamage(Damage);
            }
            BulletsCurrent--;

            yield return new WaitForSeconds(60/Rate);
            readyToShoot = true;
        }
        
    }
}
