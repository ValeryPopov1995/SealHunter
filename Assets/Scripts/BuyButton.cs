using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public Weapon WeaponPrefab;
    public int Price = 10;

    public void BuyWeapon()
    {
        PlayerParams player = FindObjectOfType<PlayerParams>();
        if (player.Money >= Price)
        {
            Debug.Log("weapon was bot");
            player.Money -= Price;
            player.Weapon2 = WeaponPrefab;

            player.Weapon1.gameObject.SetActive(false);
            player.Weapon2.gameObject.SetActive(true);

            player.AmmoText.text = player.Weapon2.AmmoString;
            GetComponent<Button>().interactable = false;
        }
    }
}
