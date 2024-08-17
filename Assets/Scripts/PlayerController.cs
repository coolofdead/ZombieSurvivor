using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Points")]
    public int Points;

    [Header("Stats")]
    public int MaxHealth;
    public int health;

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
}
