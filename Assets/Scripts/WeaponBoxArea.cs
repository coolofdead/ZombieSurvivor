using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponBoxArea : MonoBehaviour
{
    public WeaponBox weaponBox;
    public GameObject deliveryBoxAnimated;
    public Transform overworldLight;

    public bool IsUnlocked;

    public void UnlockBox()
    {
        IsUnlocked = true;

        gameObject.SetActive(true);

        DOTween.Sequence()
                .Append(overworldLight.DOScaleX(1, 0.75f).SetEase(Ease.OutSine))
               .AppendInterval(16)
               .AppendCallback(() => deliveryBoxAnimated.SetActive(true))
               .AppendInterval(1.35f)
               .AppendCallback(() => deliveryBoxAnimated.SetActive(true))
               .AppendCallback(() =>
               {
                   weaponBox.gameObject.SetActive(true);
                   weaponBox.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutSine);
               })
               .AppendInterval(10)
               //.AppendCallback(() => overworldLight.DOScaleX(0, 0.75f).SetEase(Ease.OutSine))
               .Play();
    }
}
