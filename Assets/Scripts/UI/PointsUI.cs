using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PointsUI : MonoBehaviour
{
    public TMP_Text pointsTMP;

    public void UpdatePoints()
    {
        pointsTMP.text = PlayerManager.Instance.PlayerController.Points.ToString();

        if (!DOTween.IsTweening(pointsTMP.transform)) pointsTMP.transform.DOScale(Vector3.one * 0.82f, 0.09f).SetEase(Ease.OutCubic).SetLoops(2, LoopType.Yoyo);
    }
}
