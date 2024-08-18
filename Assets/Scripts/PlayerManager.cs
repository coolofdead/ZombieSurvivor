using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerController PlayerController;
    public PlayerWeaponController PlayerWeaponController;
    public FirstPersonController FirstPersonController;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitPlayer();
    }

    public void InitPlayer()
    {
        PlayerController.Points = 0;
        PlayerController.GainPoints(300);
        PlayerController.Heal(PlayerController.MaxHealth);
        PlayerWeaponController.ChangeWeapon(PlayerWeaponController.CurrentWeapon);
    }
}
