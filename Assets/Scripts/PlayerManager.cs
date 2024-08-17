using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerController PlayerController;
    public PlayerWeaponController PlayerWeaponController;

    public void Awake()
    {
        Instance = this;
    }

    public void InitPlayer()
    {
        PlayerController.Points = 0;
        PlayerController.GainPoints(300);
        PlayerController.Heal(PlayerController.MaxHealth);
        PlayerWeaponController.ChangeWeapon(PlayerWeaponController.CurrentWeapon);
    }
}
