using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public MenuView MenuView;
    public GameView GameView;
    public EndGameView EndGameView;
    public SettingsView SettingsView;

    private void Awake()
    {
        Instance = this;    
    }
}
