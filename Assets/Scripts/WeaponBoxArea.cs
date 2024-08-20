using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponBoxArea : MonoBehaviour
{
    public WeaponBox weaponBox;
    public GameObject deliveryBoxAnimated;
    public SpriteRenderer overworldLight;
    public Color finalLightColor;

    public bool IsUnlocked;

    public void UnlockBox()
    {
        IsUnlocked = true;

        gameObject.SetActive(true);

        DOTween.Sequence()
                .Append(overworldLight.transform.DOScaleX(2, 0.75f).SetEase(Ease.OutSine))
               .AppendInterval(16)
               //.AppendCallback(() => deliveryBoxAnimated.SetActive(true))
               //.AppendInterval(1.35f)
               //.AppendCallback(() => deliveryBoxAnimated.SetActive(true))
               .AppendCallback(() => weaponBox.gameObject.SetActive(true))
               .Append(weaponBox.transform.DOLocalMoveY(0, 8f).SetEase(Ease.OutSine))
               .Append(weaponBox.transform.DOScale(Vector3.one * 1.36f, 0.48f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo))
               .AppendCallback(() => weaponBox.Unlock())
               .AppendInterval(10)
               .Append(overworldLight.DOColor(finalLightColor, 1.25f).SetEase(Ease.OutSine))
               //.AppendCallback(() => overworldLight.DOScaleX(0, 0.75f).SetEase(Ease.OutSine))
               .Play();
    }
}
