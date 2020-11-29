using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuyButton : MonoBehaviour
{
    public Weapon WeaponPrefab;
    public int Price = 10;

    Button btn;
    SoundEvents sound;

    private void Start()
    {
        sound = FindObjectOfType<SoundEvents>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(BuyWeapon);
    }

    public void BuyWeapon()
    {
        PlayerParams player = FindObjectOfType<PlayerParams>();
        if (player.Money >= Price && player.Weapon2 != WeaponPrefab)
        {
            Debug.Log("weapon was bot");

            if (player.Weapon2 != null) player.Weapon2.gameObject.SetActive(false);

            player.AddMoney(-Price);
            player.Weapon2 = WeaponPrefab;

            player.Weapon1.gameObject.SetActive(false);
            player.Weapon2.gameObject.SetActive(true);

            player.AmmoText.text = player.Weapon2.AmmoString;
            // btn.interactable = false;

            sound.Buy();
        }
    }
}
