using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class INFmanager : MonoBehaviour
{

  // public static bool playerExist = true;

  public static INFmanager instance;
  GameObject player_prefab;
  GameObject player;
  GameObject goal_prefab;
  GameObject goal;

  private int randPos = 0;

  public void Awake(){

    if(instance == null){
      instance = this;
    }
  }

  void Start()
  {
    player_prefab = Resources.Load<GameObject>("Player");
    goal_prefab = Resources.Load<GameObject>("Goal");
    initPlayer();
    initGoal();
  }

  //Player生成
  public void initPlayer(){

    player = Instantiate(player_prefab);
    player.transform.position = new Vector3(0, 8, 0);
  }

  //Goal生成
  public void initGoal(){

    goal = Instantiate(goal_prefab);
    goal.transform.position = new Vector3(-4, 2, -4);
  }

  //Player位置変え
  public void respawnPlayer(){

    // player = Instantiate(player_prefab);
    player.transform.position = new Vector3(0, 3, 0);
    PlayerController.instance.isMove = false;
  }

  //Goal位置変え
  public void respawnGoal(){

    randPos = Random.Range(0, 3);

    switch(randPos){

      case 0:
        goal.transform.position = new Vector3(-4, 2, -4);
        AddController.instance.getGoalPos(0);
        return;

      case 1:
        goal.transform.position = new Vector3(4, 2, -4);
        AddController.instance.getGoalPos(1);
        return;

      case 2:
        goal.transform.position = new Vector3(-4, 2, 4);
        AddController.instance.getGoalPos(2);
        return;

      case 3:
        goal.transform.position = new Vector3(4, 2, 4);
        AddController.instance.getGoalPos(3);
        return;
    }
  }



  public void initGame(){

    //壁を破壊
    DestroyWall.instance.destroyWall();
    // DestroyGoal.instance.destroyGoal();

    //壁とプレイヤーを生成
    MakeMazeINF.instance.createMaze();
    respawnPlayer();
    respawnGoal();
  }
}
