using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddController : MonoBehaviour
{

  [SerializeField] private Text addCountText;
  [SerializeField] private GameObject Goal;

  private Vector3 goalPos;
  private GameObject canvas;
  private int pos;

  public static AddController instance;

  public void Awake(){
    if(instance == null){
      instance = this;
    }
  }

  void Start(){

    // goalPos = Goal.GetComponent<Transform>().position;
    canvas = GameObject.Find("Canvas");
  }

  public void getGoalPos(int i){
    pos = i;
  }

  //秒数追加Text 表示
  public void whenGoal(){

    Text text;
    // text = Instantiate(addCountText, new Vector3(goalPos.x, goalPos.y+3.0f, goalPos.z), Quaternion.identity);
    text = Instantiate(addCountText, new Vector3(-4, 0, -4), Quaternion.identity);
    text.transform.SetParent(canvas.transform, false);

    switch(pos){

      case 0:
        text.transform.position = new Vector3(-4, 3, -4);
        return;

      case 1:
        text.transform.position = new Vector3(4, 3, -4);
        return;

      case 2:
        text.transform.position = new Vector3(-4, 3, 4);
        return;

      case 3:
        text.transform.position = new Vector3(4, 3, 4);
        return;
    }
  }

}
