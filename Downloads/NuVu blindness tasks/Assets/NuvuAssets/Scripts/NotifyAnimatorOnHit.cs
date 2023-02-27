using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyAnimatorOnHit : HitTarget
{
    public Animator animator;
    int numHits = 0;
    int numHolding = 0;

    void Start()
    {
    }

    override public void Hit(Transform source)
    {
        numHits++;
        numHolding++;
        animator.SetInteger("NumHits", numHits);
        animator.SetBool("Holding", true);
    }

    override public void Release(Transform source)
    {
        numHolding--;
        animator.SetBool("Holding", numHolding != 0);
    }
}
