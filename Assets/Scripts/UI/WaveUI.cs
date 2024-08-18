using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveTMP;
    public TMP_Text zombiesLeftTMP;
    public TMP_Text weaponDeliveringTMP;

    public void UpdateZombiesLeft()
    {
        if (!DOTween.IsTweening(zombiesLeftTMP.transform)) zombiesLeftTMP.transform.DOPunchScale(Vector3.one * 1.12f, 0.35f).SetEase(Ease.OutSine);
        zombiesLeftTMP.text = $"{WaveMaanger.Insntance.ZombiesLeft} zombies left";
    }

    public void UpdateWave()
    {
        waveTMP.transform.DOPunchScale(Vector3.one * 1.12f, 0.35f).SetEase(Ease.OutSine);
        waveTMP.text = $"Wave : {WaveMaanger.Insntance.CurrentWave}";
    }

    public void ShowWeaponBoxDelivering()
    {
        DOTween.Sequence().Append(weaponDeliveringTMP.DOFade(1, 0.95f).SetEase(Ease.InQuart)).AppendInterval(1f).SetLoops(8, LoopType.Yoyo).Play();
    }
}
