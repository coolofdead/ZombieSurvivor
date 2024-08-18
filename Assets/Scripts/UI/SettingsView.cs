using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsView : MonoBehaviour
{
    public GameObject content;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new();
        controls.Enable();

        controls.Player.Escape.performed += _ => 
        {
            if (GameManager.Instance.IsGamePaused) 
                HideSettings();
            else 
                ShowSettings();
        };
    }

    public void ShowSettings()
    {
        content.SetActive(true);

        Time.timeScale = 0;
        GameManager.Instance.IsGamePaused = true;
    }

    public void HideSettings()
    {
        content.SetActive(false);

        Time.timeScale = 1;
        GameManager.Instance.IsGamePaused = false;
    }
}
