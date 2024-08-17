using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public List<SpriteRenderer> wheatSRs;

    public float wheatOpacityOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        foreach (var wheatSR in wheatSRs)
        {
            wheatSR.DOKill();
            wheatSR.DOFade(wheatOpacityOnTrigger, 0.35f).SetEase(Ease.OutSine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        foreach (var wheatSR in wheatSRs)
        { 
            wheatSR.DOKill();
            wheatSR.DOFade(1, 0.35f).SetEase(Ease.OutSine);
        }
    }
}
