using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    public List<Image> perksUI;

    public void UpdatePerks()
    {
        for (int i = 0; i < perksUI.Count; i++)
        {
            perksUI[i].gameObject.SetActive(i < PlayerManager.Instance.PlayerController.perks.Count);

            if (i < PlayerManager.Instance.PlayerController.perks.Count)
            {
                perksUI[i].sprite = PlayerManager.Instance.PlayerController.perksSO[i].ico;
            }
        }
    }
}
