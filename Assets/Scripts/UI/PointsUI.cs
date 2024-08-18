using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public TMP_Text pointsTMP;

    public void UpdatePoints()
    {
        pointsTMP.text = PlayerManager.Instance.PlayerController.Points.ToString();
    }
}
