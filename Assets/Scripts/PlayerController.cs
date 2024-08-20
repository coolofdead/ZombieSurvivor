using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Points")]
    public int Points;

    [Header("Stats")]
    public int MaxHealth;
    public int health;

    public List<PerkType> perks = new();
    public List<PerkSO> perksSO = new();

    [Header("Regen")]
    public int healthRegen = 1;
    public float delayBetweenRegen = 0.15f;
    public int delayBeforeRegenInSec = 5;

    private float lastHitTime;
    private float lastRegenTime;

    private void Update()
    {
        if (health == MaxHealth) return;

        if (lastHitTime + delayBeforeRegenInSec > Time.time) return;

        if (lastRegenTime + delayBetweenRegen > Time.time) return;

        lastRegenTime = Time.time;
        Heal(healthRegen);
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, MaxHealth);

        UIManager.Instance.GameView.HealthUI.UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        if (perks.Contains(PerkType.Drug)) damage -= 5;

        health = Mathf.Clamp(health - damage, 0, MaxHealth);
        lastHitTime = Time.time;

        if (health <= 0)
        {
            GameManager.Instance.ChangeState(GameManager.State.EndGame);
        }

        UIManager.Instance.GameView.HealthUI.UpdateHealth();
    }

    public void GainPoints(int amount)
    {
        Points += amount;

        UIManager.Instance.GameView.PointsUI.UpdatePoints();
    }

    public void UnlockPerk(PerkSO perkSO)
    {
        perksSO.Add(perkSO);
        perks.Add(perkSO.perkType);

        UIManager.Instance.GameView.PerkUI.UpdatePerks();

        if (perkSO.perkType == PerkType.BloodPack)
        {
            MaxHealth += 50;
        }

        if (perkSO.perkType == PerkType.Medkit)
        {
            healthRegen += 1;
        }

        if (perkSO.perkType == PerkType.Syringe)
        {
            delayBeforeRegenInSec -= 2;
        }

        if (perkSO.perkType == PerkType.Juice)
        {
            if (TryGetComponent(out FirstPersonController firstPersonController))
            {
                firstPersonController.MoveSpeed += 1.5f;
                firstPersonController.SprintSpeed += 0.5f;
            }
        }

        if (perkSO.perkType == PerkType.Coffee)
        {
            if (TryGetComponent(out FirstPersonController firstPersonController))
            {
                firstPersonController.MaxSprintEnergy += 240f;
            }
        }

        if (perkSO.perkType == PerkType.Melon)
        {
            if (TryGetComponent(out FirstPersonController firstPersonController))
            {
                firstPersonController.SprintEnergyRecoverPerSec += 0.5f;
            }
        }
    }
}
