using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontroller : MonoBehaviour
{

  public static BGMcontroller instance;

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

  public void StopBGM(){
    audioSource.Stop();
  }

}
