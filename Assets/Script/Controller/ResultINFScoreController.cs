using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultINFScoreController : MonoBehaviour
{

  public int Score = 0;
  public int disScore = 0;
  public Text ScoreValue;

  void Start(){
    Score = ScoreController.instance.getScore();
    ScoreValue.text = string.Format("{0}", disScore);
    naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score);
  }

  void Update(){

    if(disScore < Score){
      disScore += 100;
      ScoreValue.text = string.Format("{0}", disScore);
    }
    if(disScore > Score){
      disScore = Score;
      ScoreValue.text = string.Format("{0}", disScore);
    }
  }

}
