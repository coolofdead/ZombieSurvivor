using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public TMP_Text valueTMP;
    public AudioMixer AudioMixer;

    public void OnValueChanged(float newValue)
    {
        valueTMP.text = newValue.ToString("0.0");

        AudioMixer.SetFloat("Global", newValue);
    }
}
