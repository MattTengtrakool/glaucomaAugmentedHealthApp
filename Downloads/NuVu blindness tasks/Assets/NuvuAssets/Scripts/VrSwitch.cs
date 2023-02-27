using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VrSwitch : MonoBehaviour
{
    public GameObject[] nonVrObjects;
    public GameObject[] vrObjects;

    void Awake()
    {
        bool vrEnabled = VRSettings.enabled;

        foreach (GameObject nonVr in nonVrObjects)
            nonVr.SetActive(!vrEnabled);

        foreach (GameObject vr in vrObjects)
            vr.SetActive(vrEnabled);
    }
}
