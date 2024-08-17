using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LeaveBoxBtn : MonoBehaviour, IShootable
{
    public GameObject hoverOutline;

    public int GetPointForHit()
    {
        return 0;
    }

    public int GetPointForKill()
    {
        return 0;
    }

    public void Hit(int damage)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public bool IsDead()
    {
        return true;
    }

    private void OnMouseEnter()
    {
        hoverOutline.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverOutline.SetActive(false);
    }
}