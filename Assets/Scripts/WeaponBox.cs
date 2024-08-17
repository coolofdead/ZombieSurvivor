using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public WeaponSO weaponToBuy;
    public TMP_Text buyTMP;

    public SpriteRenderer weaponSR;

    private PlayerControls controls;
    private bool isPlayerInRangeToBuy;

    private void Start()
    {
        controls = new();
        controls.Enable();
        controls.Player.BuyWeapon.performed += _ => BuyWeapon();

        weaponSR.transform.DOLocalMoveY(weaponSR.transform.localPosition.y + 0.12f, 1.75f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void BuyWeapon()
    {
        if (!isPlayerInRangeToBuy || PlayerManager.Instance.PlayerController.Points < weaponToBuy.price) return;

        PlayerManager.Instance.PlayerController.GainPoints(-weaponToBuy.price);
        PlayerManager.Instance.PlayerWeaponController.ChangeWeapon(weaponToBuy);
    }

    public void ChangeWeapon(WeaponSO weaponSO)
    {
        weaponToBuy = weaponSO;

        weaponSR.sprite = weaponToBuy.weaponSprite;
        buyTMP.text = $"Press \"F\"\nto buy : {weaponToBuy.price}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        buyTMP.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutSine);
        isPlayerInRangeToBuy = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        buyTMP.transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.OutSine);
        isPlayerInRangeToBuy = false;
    }
}
