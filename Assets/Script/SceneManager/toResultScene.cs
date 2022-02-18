using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toResultScene : MonoBehaviour
{

  public static float score;

  void OnTriggerEnter(Collider collision){

    //goalのパーティクルを待ってから画面遷移
    if(collision.gameObject.tag == "Player"){
      CountDown.instance.reachGoal();
      // score = CountDown.CountDownTime;
      Invoke("ChangeScene", 2f);
    }
  }

  void ChangeScene(){
    SceneManager.LoadScene("Result");
  }
}
