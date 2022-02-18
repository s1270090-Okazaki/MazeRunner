using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public static ScoreController instance;
    public int Score;
    public Text TextScore;
    private int i;  //Score調整用

    public void Awake(){

      if(instance == null){
        instance = this;
      }
    }

    void Start(){

      Score = 0;
      TextScore.text = string.Format("Score : {0}", Score);
    }

    void Update(){

      TextScore.text = string.Format("Score : {0}", Score);
      if(i>0 && i<=20){
        Score += 5;
        i++;
      }
    }

    public void addScore(){

      // Score += 100f;
      i = 1;
    }

    public int getScore(){

      return Score;
    }
}
