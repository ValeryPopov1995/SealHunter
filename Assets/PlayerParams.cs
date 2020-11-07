using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParams : MonoBehaviour
{
    public Weapon Weapon1, Weapon2;
    
    public void SwitchWeapon()
    {
        if (Weapon2 != null && !Weapon2.enabled)
        {
            Weapon1.enabled = false;
            Weapon2.enabled = true;
        }
        else
        {
            Weapon1.enabled = true;
            Weapon2.enabled = false;
        }
    }

    public void Shoot()
    {
        if (Weapon1.enabled) StartCoroutine(Weapon1.Shoot());
    }
}
