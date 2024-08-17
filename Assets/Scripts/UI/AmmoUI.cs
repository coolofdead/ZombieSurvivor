using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public BulletUI bulletUIPrefab;
    public Transform ammoParent;
    public TMP_Text ammoTMP;
    public GameObject reloadTMP;

    private List<BulletUI> bulletUIs = new();

    public void UseAmmo()
    {
        var bulletUI = bulletUIs.First(bulletUI => !bulletUI.IsUsed);
        bulletUI.Use();

        UpdateAmmoTMP();
    }

    public void RefreshAmmos()
    {
        foreach (var bulletUI in bulletUIs)
        {
            bulletUI.Refill();
        }

        UpdateAmmoTMP();
    }

    public void UpdateUI()
    {
        foreach (Transform bullet in ammoParent)
        {
            Destroy(bullet.gameObject);
        }

        bulletUIs.Clear();

        for (int i = 0; i < PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.MaxAmmo; i++)
        {
            var newAmmo = Instantiate(bulletUIPrefab, ammoParent);
            bulletUIs.Add(newAmmo);
        }

        RefreshAmmos();
    }

    private void UpdateAmmoTMP()
    {
        ammoTMP.text = $"Ammo: {PlayerManager.Instance.PlayerWeaponController.CurrentAmmo} I {PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.MaxAmmo}";

        reloadTMP.SetActive(bulletUIs.All(bulletUI => bulletUI.IsUsed));
    }
}
