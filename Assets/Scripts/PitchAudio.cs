using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchAudio : MonoBehaviour
{
    public bool CreateSource = true;
    public AudioClip Clip;
    public AudioClip[] Clips;
    [Range(0f, 1f)]
    public float Verity = 1f;

    AudioSource src;

    private void Start()
    {
        if (Verity != 1f)
        {
            float p = Random.Range(0f, 1f);
            if (p > Verity) return;
        }

        if (CreateSource) src = gameObject.AddComponent<AudioSource>();
        else src = GetComponent<AudioSource>();
        
        // if (Clip.Length > 0) src.clip = Clip[Random.Range(0,Clip.Length)];
        if (Clip != null)
        {
            src.clip = Clip;
            src.Play();
        }
        else if (Clips.Length > 0)
        {
            src.clip = Clips[Random.Range(0, Clips.Length)];
            src.Play();
        }
        src.pitch = Random.Range(.8f, 1.2f);
    }
}
