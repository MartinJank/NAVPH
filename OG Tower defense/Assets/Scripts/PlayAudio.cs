using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audio;

    public void playThisSound(){
        audio.Play();
    }
}
