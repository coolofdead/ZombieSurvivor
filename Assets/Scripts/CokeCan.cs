using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeCan : MonoBehaviour, IShootable
{
    public Animator cokeAnimator;
    public Rigidbody rb;

    public float shotForce = 6f;

    private bool hasGivenPoints;

    public int GetPointForHit()
    {
        return !hasGivenPoints ? 10 : 0;
    }

    public int GetPointForKill()
    {
        return 0;
    }

    public void Hit(int damage)
    {
        hasGivenPoints = true;

        cokeAnimator.SetTrigger("Shoot");

        rb.AddForce((-transform.forward + transform.up) * shotForce, ForceMode.Impulse);
    }

    public bool IsDead()
    {
        return false;
    }
}
