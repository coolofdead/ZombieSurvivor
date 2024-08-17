using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthFill;
    public TMP_Text healthTMP;

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
    }
}
