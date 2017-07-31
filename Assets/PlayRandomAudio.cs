using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour {

    public List<AudioClip> clips;

    private AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        src.clip = clips[Random.Range(0, clips.Count)];
        src.Play();
    }
}
