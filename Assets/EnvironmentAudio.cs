using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAudio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioSource()
    {
        audioSource.enabled = true;
    }

    public void DisableAudioSource()
    {
        audioSource.enabled = false;
    }
}
