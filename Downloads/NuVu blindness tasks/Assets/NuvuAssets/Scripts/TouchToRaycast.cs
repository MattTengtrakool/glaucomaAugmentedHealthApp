using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToRaycast : MonoBehaviour
{
    public struct TouchHitInfo {
        public HitTarget[] hitTargets;
        public Vector3 startDirection;
        public Vector3 lastDirection;
        public float distance;
    }

    public Transform playerTransform;
    Camera cameraComponent;

    Dictionary<int, TouchHitInfo> currentHits;

    Ray GetCameraRay(Vector2 screenPosition)
    {
        return cameraComponent.ScreenPointToRay(screenPosition);
    }

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        Input.multiTouchEnabled = true;
        currentHits = new Dictionary<int, TouchHitInfo>();

        if (playerTransform == null)
            playerTransform = transform;
    }

    void ReceiveTouchBegin(Vector2 position, int id)
    {
        Ray ray = GetCameraRay(position);
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

                TouchHitInfo info = new TouchHitInfo();
                info.hitTargets = targets;
                info.startDirection = ray.direction;
                info.lastDirection = ray.direction;
                info.distance = hit.distance;
                currentHits[id] = info;
            }
        }
    }

    void ReceiveTouchEnd(Vector2 position, int id)
    {
        if (!currentHits.ContainsKey(id))
            return;

        HitTarget[] targets = currentHits[id].hitTargets;
        foreach (HitTarget target in targets)
        {
            if (target)
                target.Release(playerTransform);
        }
        currentHits.Remove(id);
    }

    void ReceiveTouchMove(Vector2 position, int id)
    {
        if (!currentHits.ContainsKey(id))
            return;

        Ray ray = GetCameraRay(position);
        TouchHitInfo info = currentHits[id];
        HitTarget[] targets = info.hitTargets;
        foreach (HitTarget target in targets)
        {
			if (target) {
				target.Move (playerTransform, info.distance, info.lastDirection, ray.direction);

			}
        }

        info.lastDirection = ray.direction;
        currentHits[id] = info;
    }

    void ReceiveTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
            ReceiveTouchBegin(touch.position, touch.fingerId);
        else if (touch.phase == TouchPhase.Ended)
            ReceiveTouchEnd(touch.position, touch.fingerId);
        else if (touch.phase == TouchPhase.Moved)
            ReceiveTouchMove(touch.position, touch.fingerId);
    }

    void UpdateTouches()
    {
        for (int i = 0; i < Input.touchCount; ++i)
            ReceiveTouch(Input.touches[i]);
    }

    void UpdateMouse()
    {
        Vector2 position = new Vector2(cameraComponent.pixelWidth / 2.0f, cameraComponent.pixelHeight / 2.0f);
        if (Input.GetMouseButtonDown(0))
            ReceiveTouchBegin(position, 0);
        if (Input.GetMouseButtonUp(0))
            ReceiveTouchEnd(position, 0);
        if (Input.GetMouseButton(0))
            ReceiveTouchMove(position, 0);
    }

    void Update()
    {
        if (cameraComponent == null)
            return;

        if (Input.touchSupported)
            UpdateTouches();
        else
            UpdateMouse();
    }
}
