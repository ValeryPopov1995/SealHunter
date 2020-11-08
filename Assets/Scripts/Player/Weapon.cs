using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage = 10, Rate = 100, ReloadTime = 1f;
    public int BulletsMax = 10, BulletsCurrent = 10;
    public ParticleSystem ShootParticles;

    bool readyToShoot = true;
    [HideInInspector]
    public string AmmoString;

    private void Awake()
    {
        AmmoString = BulletsCurrent.ToString();
    }

    public IEnumerator Shoot()
    {
        if (readyToShoot && BulletsCurrent > 0)
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
            AmmoString = BulletsCurrent.ToString();
            if (BulletsCurrent <= 0) StartCoroutine(reload());

            yield return new WaitForSeconds(60/Rate);
            readyToShoot = true;
        }
        
    }
    IEnumerator reload()
    {
        AmmoString = "Reloading";
        readyToShoot = false;
        yield return new WaitForSeconds(ReloadTime);
        BulletsCurrent = BulletsMax;
        AmmoString = BulletsCurrent.ToString();
        FindObjectOfType<PlayerParams>().AmmoText.text = AmmoString; // костыль !
        readyToShoot = true;
    }

    private void OnDisable()
    {
        StopCoroutine(reload());
    }
}
