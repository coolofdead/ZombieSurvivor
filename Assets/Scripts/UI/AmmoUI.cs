using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public const string NO_AMMO_COLOR_TAG = "<color=red>";
    public const string NO_AMMO_COLOR_END_TAG = "</color>";

    public BulletUI bulletUIPrefab;
    public Transform ammoParent;
    public TMP_Text ammoTMP;
    public GameObject reloadTMP;

    private List<BulletUI> bulletUIs = new();

    public void UseAmmo()
    {
        var bulletUI = bulletUIs.Last(bulletUI => !bulletUI.IsUsed);
        bulletUI.Use();

        UpdateAmmoTMP();
    }

    public void RefreshAmmos()
    {
        //if (PlayerManager.Instance.PlayerWeaponController.CurrentAmmo + PlayerManager.Instance.PlayerWeaponController.TotalAmmo >= PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.MaxAmmo)

        var ammoToReloads = Mathf.Clamp(PlayerManager.Instance.PlayerWeaponController.CurrentAmmo + PlayerManager.Instance.PlayerWeaponController.TotalAmmo, 0, bulletUIs.Count);
        for (int i = 0; i < ammoToReloads; i++)
        {
            if (bulletUIs[i].IsUsed) bulletUIs[i].Refill();
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
        var hasNoAmmo = PlayerManager.Instance.PlayerWeaponController.CurrentAmmo == 0 && PlayerManager.Instance.PlayerWeaponController.TotalAmmo == 0;
        ammoTMP.text = $"Ammo: {(hasNoAmmo ? NO_AMMO_COLOR_TAG : "")}{PlayerManager.Instance.PlayerWeaponController.CurrentAmmo}{(hasNoAmmo ? NO_AMMO_COLOR_END_TAG : "")} I <size=70%>{PlayerManager.Instance.PlayerWeaponController.TotalAmmo} - {PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.name}";

        reloadTMP.SetActive(bulletUIs.All(bulletUI => bulletUI.IsUsed));
    }
}
