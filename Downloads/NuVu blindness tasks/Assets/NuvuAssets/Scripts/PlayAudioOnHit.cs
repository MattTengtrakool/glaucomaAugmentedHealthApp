using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnHit : HitTarget
{
    public AudioSource hitAudioSource;
    public bool stopOnRelease = false;
    public int playOnlyOnHit = 0;

    int numHits = 0;

    void Start()
    {
        if (hitAudioSource == null)
            hitAudioSource = GetComponent<AudioSource>();
    }

    bool ShouldPlay()
    {
        return playOnlyOnHit == 0 || numHits == playOnlyOnHit;
    }

    override public void Hit(Transform source)
    {
        numHits++;

        if (hitAudioSource != null && ShouldPlay())
            hitAudioSource.Play();
    }

    override public void Release(Transform source)
    {
        if (hitAudioSource != null && stopOnRelease && ShouldPlay())
            hitAudioSource.Stop();
    }
}
