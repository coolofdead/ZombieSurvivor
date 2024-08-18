using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BulletUI : MonoBehaviour
{
    public Sprite ammoReady;
    public Sprite ammoUsed;

    public Color ammoReadyColor;
    public Color ammoUsedColor;

    public Image ammoImage;

    public bool IsUsed;

    public void Use()
    {
        IsUsed = true;

        //ammoImage.sprite = ammoUsed;
        ammoImage.DOColor(ammoUsedColor, 0.35f).SetEase(Ease.OutSine);

        transform.DOKill();
        transform.DOPunchScale(new Vector3(1, 0, 1), 0.35f, 1, 0.12f).SetEase(Ease.OutSine);
    }

    public void Refill()
    {
        IsUsed = false;

        //ammoImage.sprite = ammoReady;
        ammoImage.DOColor(ammoReadyColor, 0.55f).SetEase(Ease.OutSine);

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(new Vector3(1, 1.16f, 1), 0.35f, 1, 0.12f).SetEase(Ease.OutSine);
    }
}
