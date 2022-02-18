using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownINF : MonoBehaviour
{

  public static CountDownINF instance;

  //Text
  public float CountDownTime;
  public Text TextCountDown;

  //SE
  private bool OnSound = false;
  public AudioClip SE_Death;
  AudioSource audioSource;

  public void Awake(){
    if(instance == null){
      instance = this;
    }
  }

  void Start(){
    CountDownTime = 10.0f;
    audioSource = GetComponent<AudioSource>();
    TextCountDown.text = string.Format("{0:00.00}", CountDownTime);
  }

  void Update(){

    //Ground常にいる時だけカウントが進む
    if(PlayerController.instance.OnGround){
      TextCountDown.text = string.Format("{0:00.00}", CountDownTime);
      CountDownTime -= Time.deltaTime;

      if(CountDownTime <= 0.0f){
        CountDownTime = 0.0f;
        PlayerController.instance.isMove = false;
        if(!OnSound){
          BGMcontroller.instance.StopBGM();
          SEcontroller.instance.StopSE();
          audioSource.PlayOneShot(SE_Death);
          OnSound = true;
        }
        Invoke("ChangeScene", 2f);
      }
    }

  }

  public void addCount(){
    CountDownTime += 3.0f;
  }

  void ChangeScene(){
    SceneManager.LoadScene("ResultINF");
  }

}
