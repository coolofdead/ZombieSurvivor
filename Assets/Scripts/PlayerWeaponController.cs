using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    public WeaponSO CurrentWeapon;
    public int CurrentAmmo;
    public int TotalAmmo;

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
        if (controls.Player.Shoot.IsPressed() && CurrentWeapon.gunType == GunType.Automatic)
        {
            Shoot();
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        Shoot();
    }

    public void Shoot()
    {
        if (lastShootTime + CurrentWeapon.FireRate > Time.time) return;

        if (CurrentAmmo == 0)
        {
            audioSource.clip = CurrentWeapon.emptyMagazineClip;
            audioSource.Play();

            return;
        }

        CurrentAmmo--;
        lastShootTime = Time.time;

        audioSource.clip = CurrentWeapon.shootClip;
        audioSource.Play();

        UIManager.Instance.GameView.AmmoUI.UseAmmo();
        UIManager.Instance.GameView.WeaponUI.Shoot();
        GameManager.Instance.CurrentGameData.totalBulletsShot++;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent(out IShootable shootable))
            {
                var perkBonusDamage = 0;
                switch (CurrentWeapon.gunStyle)
                {
                    case GunStyle.Pistol:
                        if (PlayerManager.Instance.PlayerController.perks.Contains(PerkType.HeavyPistolBullet)) perkBonusDamage = 15;
                        break;
                    case GunStyle.Rifle:
                        if (PlayerManager.Instance.PlayerController.perks.Contains(PerkType.HeavyRifleBullet)) perkBonusDamage = 15;
                        break;
                    case GunStyle.Shotgun:
                        if (PlayerManager.Instance.PlayerController.perks.Contains(PerkType.HeavyShotgunBullet)) perkBonusDamage = 15;
                        break;
                    case GunStyle.SMG:
                        if (PlayerManager.Instance.PlayerController.perks.Contains(PerkType.HeavySMGBullet)) perkBonusDamage = 15;
                        break;
                    case GunStyle.Sniper:
                        if (PlayerManager.Instance.PlayerController.perks.Contains(PerkType.HeavySniperBullet)) perkBonusDamage = 15;
                        break;
                }

                var bodyPartHit = shootable.GetBodyPart(hit.point);
                var damageDone = (int)(CurrentWeapon.Damage * CurrentWeapon.DamageMultiplicator(bodyPartHit)) + perkBonusDamage;

                shootable.Hit(damageDone);
                UIManager.Instance.GameView.HealthUI.ShowDamageHit(damageDone, bodyPartHit == BodyPart.Head);
                GameManager.Instance.CurrentGameData.totalDamageDealt += damageDone;

                if (bodyPartHit == BodyPart.Head) GameManager.Instance.CurrentGameData.totalHeadshot++;

                PlayerManager.Instance.PlayerController.GainPoints(shootable.IsDead() ? shootable.GetPointForKill() : shootable.GetPointForHit());
                if (CurrentWeapon.gunPerk.HasFlag(GunPerk.ExplodingShot))
                {
                    var aoeHits = Physics.SphereCastAll(new Ray(hit.point, Vector3.one), 5f);
                    foreach (var aoeHit in aoeHits)
                    {
                        if (hit.transform.TryGetComponent(out IShootable aoeEhootable))
                        {
                            aoeEhootable.Hit(CurrentWeapon.Damage);
                        }
                    }
                }

                if (!CurrentWeapon.gunPerk.HasFlag(GunPerk.PiercingShot)) break;
            }
        }
    }

    public void ChangeWeapon(WeaponSO weaponSO)
    {
        controls.Player.Shoot.performed -= Shoot;

        CurrentWeapon = weaponSO;
        TotalAmmo = weaponSO.MaxAmmo * weaponSO.StartMagazine;
        lastShootTime = 0;

        if (CurrentWeapon.gunType == GunType.SemiAuto)
        {
            controls.Player.Shoot.performed += Shoot;
        }

        UIManager.Instance.GameView.WeaponUI.ChangeWeapon();
        UIManager.Instance.GameView.AmmoUI.UpdateUI();

        Reload();
    }

    public void Reload()
    {
        if (CurrentAmmo == CurrentWeapon.MaxAmmo || TotalAmmo == 0) return;

        var ammoToReload = Mathf.Clamp(CurrentWeapon.MaxAmmo - CurrentAmmo, 0, TotalAmmo);
        CurrentAmmo = CurrentAmmo + ammoToReload;
        TotalAmmo -= ammoToReload;

        audioSource.clip = CurrentWeapon.reloadClip;
        audioSource.Play();

        UIManager.Instance.GameView.WeaponUI.Reload();
        UIManager.Instance.GameView.AmmoUI.RefreshAmmos();
    }
}
