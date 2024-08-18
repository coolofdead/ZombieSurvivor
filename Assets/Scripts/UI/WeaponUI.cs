using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Animator gunAnimator;

    public Image gunRightSide, gunLeftSide;

    public void Shoot()
    {
        gunAnimator.Play("Shoot");
    }

    public void Reload()
    {
        gunAnimator.SetTrigger("reload");
    }

    public void ChangeWeapon()
    {
        gunAnimator.Play("Swap");

        gunAnimator.SetFloat("gunFireRate", 1 / PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.FireAnimationSpeed);
    }

    public void UpdateGunUI()
    {
        gunRightSide.sprite = PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.weaponSprite;
        gunLeftSide.sprite = PlayerManager.Instance.PlayerWeaponController.CurrentWeapon.weaponSprite;
    }
}
