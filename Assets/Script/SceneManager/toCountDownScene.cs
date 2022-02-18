using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toCountDownScene : MonoBehaviour
{

  AudioSource audioSource;

  void Start(){
    audioSource = GetComponent<AudioSource>();
  }

  public void OnClickStartButton(){
    audioSource.Play();
    StartCoroutine("ChangeScene");
  }

  IEnumerator ChangeScene(){
    yield return new WaitForSeconds(0.3f);
    SceneManager.LoadScene("CountDown");
  }
}
