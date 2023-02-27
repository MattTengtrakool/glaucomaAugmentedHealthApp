using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    void Start()
    {
    }

    virtual public void Hit(Transform source)
    {
    }

    virtual public void Release(Transform source)
    {
    }

    virtual public void Move(Transform source, float distance, Vector3 lastDirection, Vector3 newDirection)
    {
    }
}
