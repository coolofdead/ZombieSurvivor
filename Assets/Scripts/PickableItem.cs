using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    protected virtual void Start()
    {
        transform.DOMoveY(transform.position.y + 0.12f, 1.75f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Pickup();

        transform.DOScale(Vector3.zero, 0.75f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOKill();
            gameObject.SetActive(false);
        });
    }

    public abstract void Pickup();
}
