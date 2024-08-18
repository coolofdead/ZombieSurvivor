using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseSensivitySlider : MonoBehaviour
{
    public TMP_Text valueTMP;

    public void OnValueChanged(float newValue)
    {
        valueTMP.text = newValue.ToString();
        PlayerManager.Instance.GetComponent<FirstPersonController>().MouseRotationMultiplier = newValue;
    }
}
