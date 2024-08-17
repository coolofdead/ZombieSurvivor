using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameView GameView;
    public MenuView MenuView;

    private void Awake()
    {
        Instance = this;    
    }
}
