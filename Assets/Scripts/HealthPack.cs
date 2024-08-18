using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : PickableItem
{
    public int healAmount;

    public override void Pickup()
    {
        PlayerManager.Instance.PlayerController.Heal(healAmount);
    }
}
