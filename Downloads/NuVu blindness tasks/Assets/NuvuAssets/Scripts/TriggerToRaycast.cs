using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToRaycast : MonoBehaviour
{
    public Transform triggerSource;
    public Transform playerTransform;

    HitTarget[] currentHits = new HitTarget[0];

    void Start()
    {
        if (triggerSource == null)
            triggerSource = transform;
        if (playerTransform == null)
            playerTransform = transform;
    }

    void Trigger()
    {
        Ray ray = new Ray(triggerSource.position, triggerSource.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Collider hitCollider = hit.collider;
            Rigidbody hitBody = hit.rigidbody;

            if (hitCollider)
            {
                HitTarget[] targets = hitCollider.GetComponents<HitTarget>();
                foreach (HitTarget target in targets)
                    target.Hit(playerTransform);
                currentHits = targets;
            }
        }
    }

    void Release()
    {
        foreach (HitTarget currentHit in currentHits)
            currentHit.Release(playerTransform);

        currentHits = new HitTarget[0];
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            Trigger();
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            Release();
    }
}
