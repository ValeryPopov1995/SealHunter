using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Rate = 100, ReloadTime = 1f, AccuracyAngle = 1f;
    public int BulletsPerShoot = 1, BulletsMax = 10, BulletsCurrent = 10;
    public GameObject BulletPrefab, FireEffectPrefab;

    bool readyToShoot = true, reloading = false;
    [HideInInspector]
    public string AmmoString;
    Coroutine routine;
    PlayerParams player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerParams>();
        AmmoString = BulletsCurrent.ToString();
        if (FireEffectPrefab != null) FireEffectPrefab.SetActive(false);
    }

    public IEnumerator Shoot()
    {
        if (readyToShoot && BulletsCurrent > 0 && !reloading)
        {
            readyToShoot = false;

            for (int i = 0; i < BulletsPerShoot; i++)
            {
                float addRandom = Random.Range(-AccuracyAngle, AccuracyAngle);
                Instantiate(BulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, addRandom, 0));
            }

            
            if (FireEffectPrefab != null)
            {
                FireEffectPrefab.SetActive(true);
                yield return new WaitForSeconds(60 / Rate / 10);
                FireEffectPrefab.SetActive(false);
            }

            #region or ray cast shooting
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
            #endregion

            BulletsCurrent--;
            AmmoString = BulletsCurrent.ToString();

            yield return new WaitForSeconds(60 / Rate / 10 * 9);
            
            readyToShoot = true;
        }

        if (BulletsCurrent < 1) Reload();
    }
    IEnumerator reload()
    {
        if (BulletsCurrent != BulletsMax)
        {
            AmmoString = "Reloading";
            reloading = true;
            yield return new WaitForSeconds(ReloadTime);
            BulletsCurrent = BulletsMax;
            AmmoString = BulletsCurrent.ToString();
            player.AmmoText.text = AmmoString; // костыль !
            reloading = false;
            routine = null;
        }
    }

    public void Reload()
    {
        if (routine == null) routine = StartCoroutine(reload());
    }

    public void StopReload()
    {
        if (routine != null)
        {
            AmmoString = BulletsCurrent.ToString();
            player.AmmoText.text = AmmoString; // костыль !
            reloading = false;
            StopCoroutine(routine);
            routine = null;
        }
    }
}
