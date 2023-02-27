using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnHit : HitTarget
{
    public GameObject toEnable;
    public int enableOnlyOnHit = 0;

    int numHits = 0;

    void Start()
    {
    }

    bool ShouldEnable()
    {
        return enableOnlyOnHit == 0 || numHits == enableOnlyOnHit;
    }

    void EnableObject()
    {
        toEnable.SetActive(true);
    }

    override public void Hit(Transform source)
    {
        numHits++;

        if (toEnable != null && ShouldEnable())
            EnableObject();
    }
}
