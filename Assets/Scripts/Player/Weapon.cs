using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Rate = 100, ReloadTime = 1f;
    public int BulletsMax = 10, BulletsCurrent = 10;
    public GameObject BulletPrefab;

    bool readyToShoot = true, reloading = false;
    [HideInInspector]
    public string AmmoString;

    private void Awake()
    {
        AmmoString = BulletsCurrent.ToString();
    }

    public IEnumerator Shoot()
    {
        if (readyToShoot && BulletsCurrent > 0 && !reloading)
        {
            readyToShoot = false;

            Instantiate(BulletPrefab, transform.position, transform.rotation);

            // ray cast shoot
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
            //{
            //    // impact
            //    // Instantiate(ImpactEffect, hit.transform.position, hit.transform.rotation);

            //    // damage
            //    EnemyParams enemy = hit.transform.GetComponent<EnemyParams>();
            //    if (enemy != null) enemy.TakeDamage(Damage);
            //}

            BulletsCurrent--;
            AmmoString = BulletsCurrent.ToString();

            yield return new WaitForSeconds(60 / Rate);
            readyToShoot = true;
        }
        
        if (BulletsCurrent < 1) StartCoroutine(reload());
    }
    IEnumerator reload()
    {
        AmmoString = "Reloading";
        reloading = true;
        yield return new WaitForSeconds(ReloadTime);
        BulletsCurrent = BulletsMax;
        AmmoString = BulletsCurrent.ToString();
        FindObjectOfType<PlayerParams>().AmmoText.text = AmmoString; // костыль !
        reloading = false;
    }
}
