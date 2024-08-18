using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : PickableItem
{
    public override void Pickup()
    {
        PlayerManager.Instance.PlayerWeaponController.Reload();
    }
}
