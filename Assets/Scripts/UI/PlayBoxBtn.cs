using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoxBtn : MonoBehaviour, IShootable
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
        GameManager.Instance.ChangeState(GameManager.State.Playing);
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
