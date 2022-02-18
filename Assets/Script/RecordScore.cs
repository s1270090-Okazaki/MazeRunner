using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{

    public static RecordScore instance;
    public Text TextCountDown;
    // public float oddScore;

    public void Awake(){
      if(instance == null){
        instance = this;
      }
    }

    void Start(){
      TextCountDown.text = string.Format("BEST : {0:00.00}", toMainScene.oldScore);
    }

    public void recordScore(){
      toMainScene.scoreList.Add(ResultCountDown.score);
      if(toMainScene.oldScore == 0.0f) toMainScene.oldScore = ResultCountDown.score;
      else if(ResultCountDown.score < toMainScene.oldScore) {
        toMainScene.oldScore = ResultCountDown.score;
      }
    }

}
