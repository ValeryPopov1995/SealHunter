using UnityEngine;
using UnityEngine.UI;

public class PlayerParams : MonoBehaviour
{
    public Weapon Weapon1, Weapon2;
    public Text AmmoText;
    public int Money = 0;
    
    public void SwitchWeapon()
    {
        if (!Weapon2.gameObject.activeSelf)
        {
            Weapon1.StopAllCoroutines();
            Weapon1.gameObject.SetActive(false);
            Weapon2.gameObject.SetActive(true);

            AmmoText.text = Weapon2.BulletsCurrent.ToString();
        }
        else
        {
            Weapon2.StopAllCoroutines();
            Weapon1.gameObject.SetActive(true);
            Weapon2.gameObject.SetActive(false);

            AmmoText.text = Weapon1.BulletsCurrent.ToString();
        }
    }

    public void Shoot()
    {
        if (Weapon1.gameObject.activeSelf)
        {
            StartCoroutine(Weapon1.Shoot());
            AmmoText.text = Weapon1.AmmoString;
        }
        else
        {
            StartCoroutine(Weapon2.Shoot());
            AmmoText.text = Weapon2.AmmoString;
        }
    }
}
