using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "ScriptableObjects/Perk", order = 1)]
public class PerkSO : ScriptableObject
{
    public PerkType perkType;
    public int price;
    public Sprite ico;

    public new string name;
    public string bonusText;
}

public enum PerkType
{
    HeavyRifleBullet, // Extra damage on rifle
    HeavySMGBullet, // Extra damage on SMG
    HeavyPistolBullet, // Extra damage on pistol
    HeavySniperBullet, // Extra damage on sniper
    HeavyShotgunBullet, // Extra damage on shotgun
    BloodPack, // Extra 50 life
    Medkit, // Regen faster
    Syringe, // Regen take less time to activate
    Juice, // Extra speed,
    Coffee, // Extra stamina
    Melon, // Extra stamina regen
    Drug, // take 5 less damage per zombie hit
    SurvivePack, // jsp
}