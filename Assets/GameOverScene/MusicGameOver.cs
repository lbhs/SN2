using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameOver : MonoBehaviour
{
  public AudioSource GameOverAudioSource;
  public bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        GameOverAudioSource = GetComponent<AudioSource>();
        isPlaying = true;
        GameOverAudioSource.Play();
        GameOverAudioSource.loop = true; //test
    }

    // Update is called once per frame
    void Update()
    {

    }
}
