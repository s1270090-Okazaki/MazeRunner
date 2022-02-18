using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toMainScene : MonoBehaviour
{

  public static float oldScore = 0.0f;
  public static List<float> scoreList = new List<float>();

  public void OnClickStartButton(){
    ChangeScene();
  }

  void ChangeScene(){
    SceneManager.LoadScene("Main");
  }
}
