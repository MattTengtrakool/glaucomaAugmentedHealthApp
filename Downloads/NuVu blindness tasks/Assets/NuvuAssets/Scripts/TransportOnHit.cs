using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportOnHit : HitTarget
{
    void Start()
    {
    }

    override public void Hit(Transform source)
    {
        source.position = transform.position;
    }
}
