using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public WeaponSO CurrentWeapon;
    public int CurrentAmmo;

    public AudioSource audioSource;

    private PlayerControls controls;
    private float lastShootTime;

    private void Awake()
    {
        controls = new();
        controls.Enable();

        controls.Player.Reload.performed += _ => Reload();
    }

    private void Update()
    {
        if (controls.Player.Shoot.IsPressed())
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (CurrentAmmo == 0 || lastShootTime + CurrentWeapon.FireRate > Time.time) return;

        CurrentAmmo--;
        lastShootTime = Time.time;

        audioSource.clip = CurrentWeapon.shootClip;
        audioSource.Play();

        UIManager.Instance.GameView.AmmoUI.UseAmmo();
        UIManager.Instance.GameView.WeaponUI.Shoot();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IShootable shootable))
            {
                shootable.Hit(CurrentWeapon.Damage);

                PlayerManager.Instance.PlayerController.GainPoints(shootable.IsDead() ? shootable.GetPointForKill() : shootable.GetPointForHit());
            }
        }
    }

    public void ChangeWeapon(WeaponSO weaponSO)
    {
        CurrentWeapon = weaponSO;

        UIManager.Instance.GameView.WeaponUI.ChangeWeapon();
        UIManager.Instance.GameView.AmmoUI.UpdateUI();

        Reload();
    }

    public void Reload()
    {
        if (CurrentAmmo == CurrentWeapon.MaxAmmo) return;

        CurrentAmmo = CurrentWeapon.MaxAmmo;

        audioSource.clip = CurrentWeapon.reloadClip;
        audioSource.Play();

        UIManager.Instance.GameView.WeaponUI.Reload();
        UIManager.Instance.GameView.AmmoUI.RefreshAmmos();
    }
}
