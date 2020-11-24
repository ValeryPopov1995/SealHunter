using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerParams : MonoBehaviour
{
    public Weapon Weapon1, Weapon2;
    public Text AmmoText, MoneyText;
    public GameObject Replica;
    public int Money = 0;

    bool shootHold = false;

    private void Update()
    {
        if (shootHold) Shoot();
    }

    public void SwitchWeapon()
    {
        if (Weapon2 != null)
        {
            if (!Weapon2.gameObject.activeSelf)
            {
                Weapon1.StopReload();

                Weapon1.gameObject.SetActive(false);
                Weapon2.gameObject.SetActive(true);

                AmmoText.text = Weapon2.BulletsCurrent.ToString();
            }
            else
            {
                Weapon2.StopReload();

                Weapon1.gameObject.SetActive(true);
                Weapon2.gameObject.SetActive(false);

                AmmoText.text = Weapon1.BulletsCurrent.ToString();
            }
        }
        
    }

    public void Shoot()
    {
        if (Weapon1.gameObject.activeSelf)
        {
            StartCoroutine(Weapon1.Shoot());
            AmmoText.text = Weapon1.AmmoString;
            //if (Weapon1.FireEffectPrefab != null) Weapon1.FireEffectPrefab.SetActive(false);
        }
        else
        {
            StartCoroutine(Weapon2.Shoot());
            AmmoText.text = Weapon2.AmmoString;
            //if (Weapon2.FireEffectPrefab != null) Weapon2.FireEffectPrefab.SetActive(false);
        }
    }

    public void ReloadCurrentWeapon()
    {
        if (Weapon1.gameObject.activeSelf)
        {
            Weapon1.Reload();
            AmmoText.text = Weapon1.AmmoString;
        }
        else
        {
            Weapon2.Reload();
            AmmoText.text = Weapon2.AmmoString;
        }
    }

    public void SetHoldShootButton(bool isHold)
    {
        shootHold = isHold;
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyText.text = "$ " + Money.ToString();
    }

    public void TellReplica(string str)
    {
        if (str != "")
        {
            Replica.GetComponent<Animator>().SetTrigger("play");
            Replica.GetComponent<TextMesh>().text = str;
        }
    }
}
