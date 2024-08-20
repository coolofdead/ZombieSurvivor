using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : PickableItem
{
    public override void Pickup()
    {
        PlayerManager.Instance.PlayerWeaponController.CurrentAmmo = PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.MaxAmmo;
        PlayerManager.Instance.PlayerWeaponController.TotalAmmo = PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.MaxAmmo * PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.StartMagazine;

        UIManager.Instance.GameView.WeaponUI.Reload();
        UIManager.Instance.GameView.AmmoUI.RefreshAmmos();

        transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOKill();
            gameObject.SetActive(false);
        });
    }
}
