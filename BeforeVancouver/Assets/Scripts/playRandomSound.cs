using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRandomSound : MonoBehaviour {

    public int numClips;
    public int frequencyOfSoundsPercent;

    public AudioClip[] audioClip;
    public int randomPlay;

    private void Update()
    {
        randomPlay = Random.Range(0, 100);
        if (randomPlay < frequencyOfSoundsPercent)
        {
            triggerSound();
        }
    }

    void triggerSound()
    {
        int randSound = Random.Range(0, numClips);
        PlaySound(randSound);
    }

    void PlaySound(int clip)

    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioClip[clip];
        audio.PlayOneShot(audio.clip, 0.8f);
    }
}
