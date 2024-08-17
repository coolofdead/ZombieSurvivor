using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickableItem
{
    public WeaponSO weaponSO;

    public SpriteRenderer weaponSR;

    private void Awake()
    {
        weaponSR.sprite = weaponSO.weaponSprite;
    }

    public override void Pickup()
    {
        PlayerManager.Instance.PlayerWeaponController.ChangeWeapon(weaponSO);
    }
}
