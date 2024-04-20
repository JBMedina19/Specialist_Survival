using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<EnvironmentAudio> environmentalAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnvironmentAudio"))
        {
            environmentalAudio.Add(other.GetComponent<EnvironmentAudio>());
            for (int i = 0; i < environmentalAudio.Count; i++)
            {
                environmentalAudio[i].PlayAudioSource();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnvironmentAudio"))
        {
            for (int i = 0; i < environmentalAudio.Count; i++)
            {
                environmentalAudio[i].DisableAudioSource();
            }
            environmentalAudio.Remove(other.GetComponent<EnvironmentAudio>());
        }
    }
}
