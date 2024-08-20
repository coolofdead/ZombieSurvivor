using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform zombieParent;
    public List<Transform> zombieSpawnPoses;
    public List<WeaponBoxArea> weaponBoxes;
    public List<WeaponSO> weaponsOnMap;

    private void Start()
    {
        if (weaponsOnMap.Count < weaponBoxes.Count)
        {
            Debug.Log("Not engough weapon on Map script");
            return;
        }
        
        System.Random rnd = new();
        weaponBoxes = weaponBoxes.OrderBy(_ => rnd.Next()).ToList();
        weaponsOnMap = weaponsOnMap.OrderBy(_ => rnd.Next()).ToList();

        for (int i = 0; i < weaponBoxes.Count; i++)
        {
            weaponBoxes[i].weaponBox.ChangeWeapon(weaponsOnMap[i]);
        }
    }

    public void OnWaveFinished(int wave)
    {
        if (wave % 2 == 0)
        {
            UnlockWeaponBox();
        }
    }

    public void UnlockWeaponBox(bool showBoxDelivering = true)
    {
        if (weaponBoxes.All(weaponBox => weaponBox.IsUnlocked)) return;

        var weaponBox = weaponBoxes.First(weaponBox => !weaponBox.IsUnlocked);
        weaponBox.UnlockBox();

        if (showBoxDelivering) UIManager.Instance.GameView.WaveUI.ShowWeaponBoxDelivering();
    }
}
