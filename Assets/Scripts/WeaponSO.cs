using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    [Header("Gun")]
    public GunType gunType;
    public GunStyle gunStyle;
    public GunPerk gunPerk;
    public Sprite weaponSprite;
    public int price;

    [Header("Audio")]
    public AudioClip shootClip;
    public AudioClip emptyMagazineClip;
    public AudioClip reloadClip;

    [Header("Stats")]
    public int Damage;
    public float headDamageMultiplier = 1.6f;
    public float chestDamageMultiplier = 1.1f;
    public float legsDamageMultiplier = 1;
    public float DamageMultiplicator(BodyPart bodyPart)
    {
        return bodyPart switch
        {
            BodyPart.Head => headDamageMultiplier,
            BodyPart.Chest => chestDamageMultiplier,
            BodyPart.Legs => legsDamageMultiplier,
            _ => legsDamageMultiplier,
        };
    }

    public int MaxAmmo;
    public int StartMagazine = 5;
    public float FireRate;
    public float FireAnimationSpeed;
}

public enum GunStyle
{
    Rifle,
    SMG,
    Shotgun,
    Pistol,
    Sniper,
}

public enum GunType
{
    SemiAuto,
    Automatic,
}

[Flags]
public enum GunPerk
{
    None = 0,
    PiercingShot = 1,
    ExplodingShot = 2,
}