using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEcontroller : MonoBehaviour
{
  public static SEcontroller instance;

  AudioSource audioSource;

  public void Awake(){
    if(instance == null){
      instance = this;
    }
  }

  void Start(){
    audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }

  public void StopSE(){
    audioSource.Stop();
  }
}
