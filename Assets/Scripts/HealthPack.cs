using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : PickableItem
{
    public int healAmount;

    public override void Pickup()
    {
        if (PlayerManager.Instance.PlayerController.health == PlayerManager.Instance.PlayerController.MaxHealth) return;

        PlayerManager.Instance.PlayerController.Heal(healAmount);

        transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOKill();
            gameObject.SetActive(false);
        });
    }
}
