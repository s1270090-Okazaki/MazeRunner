using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

  public static CountDown instance;
  public static float CountDownTime;
  public Text TextCountDown;
  public GameObject target;
  public bool reach = false;

  public void Awake(){
    if(instance == null){
      instance = this;
    }
  }

  void Start()
  {
    CountDownTime = 120.0f;
  }

  void Update()
  {
    Transform tarTransform = target.transform;
    Vector3 tarPos = tarTransform.position;

    TextCountDown.text = string.Format("Time: {0:00.00}", CountDownTime);

    if(!reach){
      if(tarPos.y < 2 && tarPos.y >= 1) CountDownTime -= Time.deltaTime;
      else if(tarPos.y < -42 && tarPos.y >= -43) CountDownTime -= Time.deltaTime;
      else if(tarPos.y < -90) CountDownTime -= Time.deltaTime;
    }
    if(CountDownTime <= 0.0f){
      CountDownTime = 0.0f;
    }
    toResultScene.score = CountDownTime;
  }

  //現在の残り時間を取得
  public float GetCountDownTime(){
      return CountDownTime;
  }

  //最後のゴールに着いたかどうか
  public void reachGoal(){
    reach = true;
    // toResultScene.score = CountDownTime;
  }
}
