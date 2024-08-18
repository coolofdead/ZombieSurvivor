using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthFill;
    public Image energyFill;
    public TMP_Text healthTMP;

    public Image[] bloodHitFX;

    private Vector3 defaultHealthTMPPos;

    private void Awake()
    {
        defaultHealthTMPPos = healthTMP.transform.localPosition;
    }

    public void UpdateHealth()
    {
        healthTMP.DOKill();

        healthTMP.text = PlayerManager.Instance.PlayerController.health.ToString();
        healthTMP.transform.localPosition = defaultHealthTMPPos;
        if (!DOTween.IsTweening(healthTMP.transform)) healthTMP.transform.DOPunchPosition(healthTMP.transform.localPosition + Vector3.up * 15, 0.23f).SetEase(Ease.OutSine);

        var targetFill = (float)PlayerManager.Instance.PlayerController.health / PlayerManager.Instance.PlayerController.MaxHealth;
        healthFill.DOFillAmount(targetFill, 0.67f).SetEase(Ease.OutSine);

        var nbBloodsToShow = ((PlayerManager.Instance.PlayerController.MaxHealth - PlayerManager.Instance.PlayerController.health) / (float)PlayerManager.Instance.PlayerController.MaxHealth) * 10;
        for (int i = 0; i < bloodHitFX.Length; i++)
        {
            var bloodTargetScale = i < nbBloodsToShow ? Vector3.one : Vector3.zero;

            bloodHitFX[i].transform.DOScale(bloodTargetScale, 0.28f).SetEase(Ease.OutExpo);
        }
    }
}
