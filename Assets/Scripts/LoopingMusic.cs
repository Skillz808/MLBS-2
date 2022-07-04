using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingMusic : MonoBehaviour {

    private AudioSource audioSource;
    public float loopEnd;
    public float loopStart;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update() {
        if (!audioSource.isPlaying)
            return;

        if (audioSource.time >= loopEnd)
            audioSource.time = loopStart;
    }

}