using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCountDown : MonoBehaviour
{

  public Text TextCountDown;
  public static float score;
  public bool record;

  void Start()
  {
    score = 0.0f;
    record = false;
  }

  void Update()
  {

    if(score <= 120.0f - toResultScene.score){
      TextCountDown.text = string.Format("{0:00.00}", score);
      score += Time.deltaTime*40;
    }else{
      score = 120.0f - toResultScene.score;
      TextCountDown.text = string.Format("{0:00.00}", score);
      if(!record) {
        RecordScore.instance.recordScore();
        record = true;
      }
    }
  }
}
