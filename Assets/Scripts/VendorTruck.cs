using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VendorTruck : MonoBehaviour
{
    public static VendorTruck Instance;

    public List<PerkSO> perksToSell;
    public int extraCostPerPerk = 200;
    public TMP_Text sellingPerkTMP;
    public TMP_Text buyTMP;
    public SpriteRenderer perkSR;
    public Image perkImg;

    private System.Random rnd = new();

    private PlayerControls controls;
    private bool isPlayerInRangeToBuy;
    private PerkSO perkOfTheRound;
    private int PerkPrice => perkOfTheRound.price + PlayerManager.Instance.PlayerController.perks.Count * extraCostPerPerk;

    private void Start()
    {
        controls = new();
        controls.Enable();
        controls.Player.BuyWeapon.performed += _ => BuyWeapon();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshOffer()
    {
        var perksLeftToSell = perksToSell.Where(perk => !PlayerManager.Instance.PlayerController.perks.Contains(perk.perkType));

        if (!perksLeftToSell.Any()) return;

        perksLeftToSell = perksLeftToSell.OrderBy(_ => rnd.Next());
        perkOfTheRound = perksLeftToSell.First();

        perkSR.sprite = perkOfTheRound.ico;
        perkImg.sprite = perkOfTheRound.ico;

        perkSR.transform.DOScale(Vector3.one * 8, 0.32f).SetEase(Ease.OutSine);

        sellingPerkTMP.text = $"No bullshit, real deal!\n<color=red>{perkOfTheRound.name}</color> {perkOfTheRound.bonusText}";  
        buyTMP.text = $"Press \"F\" \nto buy : {PerkPrice}";
    }

    private void BuyWeapon()
    {
        if (!isPlayerInRangeToBuy || PlayerManager.Instance.PlayerController.perks.Contains(perkOfTheRound.perkType)) return;

        var canBuyWeapon = PlayerManager.Instance.PlayerController.Points >= PerkPrice;
        if (!canBuyWeapon)
        {
            if (!DOTween.IsTweening(buyTMP))
            {
                buyTMP.DOColor(Color.red, 0.28f).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo);
                buyTMP.transform.DOScale(Vector3.one * 1.36f, 0.28f).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo);
            }

            return;
        }

        perkSR.transform.DOScale(Vector3.zero, 0.32f).SetEase(Ease.OutSine);

        PlayerManager.Instance.PlayerController.GainPoints(-PerkPrice);
        PlayerManager.Instance.PlayerController.UnlockPerk(perkOfTheRound);
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
