using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnDrag : HitTarget
{
    public bool rotate = false;

    void Start()
    {
    }

    override public void Move(Transform source, float distance, Vector3 lastDirection, Vector3 newDirection)
    {
        Vector3 delta = distance * (newDirection - lastDirection);
        transform.position += delta;
    }
}
	