using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    [Header("Gun")]
    public Sprite weaponSprite;
    public int price;

    [Header("Audio")]
    public AudioClip shootClip;
    public AudioClip emptyMagazineClip;
    public AudioClip reloadClip;

    [Header("Stats")]
    public int Damage;
    public int MaxAmmo;
    public float FireRate; // delay until next shoot in milisec
}
