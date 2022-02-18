using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MakeMaze : MonoBehaviour
{
  public static int m_wall = 1;                    //壁
  public static int m_empty = 0;                   //通路
  public static int[,] maze_1 = new int[11,11];    //第一層 11*11
  public static int[,] maze_2 = new int[23,23];    //第二層 23*23
  public static int[,] maze_3 = new int[31,31];    //第三層 31*31
  public static int[,] ground_1 = new int[11,11];  //第一層グラウンド
  public static int[,] ground_2 = new int[23,23];  //第二章グラウンド
  public static int[,] ground_3 = new int[31,31];  //第三層グラウンド
  public int goalCount = 0;

  List<int> CDPath = new List<int>();       //掘削候補地

  //Wall生成
  void geneMaze(int[,] maze, int x, int y, int z){

    GameObject cube_prefab = Resources.Load<GameObject>("Wall");
    GameObject cube;
    for(int i=0; i<maze.GetLength(0); i++){
      for(int j=0; j<maze.GetLength(1); j++){
        if(maze[i,j] == 1){
          cube = Instantiate(cube_prefab);
          cube.transform.position = new Vector3(i+x, y, j+z);
        }
      }
    }
  }

  //Ground生成
  void geneGround(int[,] ground,  int x, int y, int z){

    GameObject ground_prefab = Resources.Load<GameObject>("Ground");
    GameObject cube;
    for(int i=0; i<ground.GetLength(0); i++){
      for(int j=0; j<ground.GetLength(1); j++){
        if(ground[i,j] == 1){
          cube = Instantiate(ground_prefab);
          cube.transform.position = new Vector3(i+x, y-1, j+z);
        }
      }
    }
  }

  //Goal生成
  void geneGoal(int[,] maze, int x, int y, int z){

    goalCount++;
    if(goalCount == 3){
      GameObject goalLast_prefab = Resources.Load<GameObject>("GoalLast");
      GameObject cube = Instantiate(goalLast_prefab);
      cube.transform.position = new Vector3(x, y, z);
    }else{
      GameObject goal_prefab = Resources.Load<GameObject>("Goal");
      GameObject cube = Instantiate(goal_prefab);
      cube.transform.position = new Vector3(x, y, z);
    }
  }

  //方向ランダム取得
  int getDir(List<string> dir){

    int dirInt = Random.Range(0, dir.Count);

    //方向の決定
    if(dir[dirInt] == "U") return 0;
    else if(dir[dirInt] == "R") return 1;
    else if(dir[dirInt] == "D") return 2;
    else if(dir[dirInt] == "L") return 3;
    else return -1;
  }

  //maze初期化
  void initMaze(int[,] maze){

    for(int i=0; i<maze.GetLength(0); i++){
      for(int j=0; j<maze.GetLength(1); j++){
        if (i == 0 || j == 0 || i == maze.GetLength(0)-1 || j == maze.GetLength(1)-1){
          maze[i,j] = m_empty;
        } else {
          maze[i,j] = m_wall;
        }
      }
    }
  }

  //maze初期化_test用
  void initMazeTest(int[,] maze){

    for(int i=0; i<maze.GetLength(0); i++){
      for(int j=0; j<maze.GetLength(1); j++){
        maze[i,j] = m_empty;
      }
    }
  }

  //ground初期化、ゴール位置に穴開ける
  void initGround(int[,] ground, int goal_x, int goal_z){

    for(int i=0; i<ground.GetLength(0); i++){
      for(int j=0; j<ground.GetLength(1); j++){
        if(i == goal_x && j == goal_z) ground[i,j] = 0;
        else ground[i,j] = 1;
      }
    }
  }

  //mazeの外壁を囲う
  void encloseMaze(int[,] maze){

    for(int i=0; i<maze.GetLength(0); i++){
      for(int j=0; j<maze.GetLength(1); j++){
        if(i == 0 || j == 0 || i == maze.GetLength(0)-1 || j == maze.GetLength(1)-1){
          maze[i,j] = m_wall;
        }
      }
    }
  }

  //通路の設定 & 掘削候補地の記録
  void SetPath(int[,] maze, int x, int y){

    maze[x,y] = m_empty;

    if(x%2 == 1 && y%2 == 1){
      CDPath.Add(x);
      CDPath.Add(y);
    }
  }

  //新たな出発点を探す
  int[] GetPos(){

    int posInt = Random.Range(0, CDPath.Count);
    if(posInt%2 == 1) posInt--;

    int[] ret = new int[2];
    ret[0] = CDPath[posInt];
    ret[1] = CDPath[posInt+1];

    CDPath.RemoveAt(posInt);
    CDPath.RemoveAt(posInt);

    return ret;
  }

  //穴掘り
  void digMaze(int[,] maze, int x, int y){

    //進める方向の確認
    while(true){
      //進行可能な方向を一時記録
      List<string> dir = new List<string>();
      //Up
      if(maze[x-1,y] == m_wall && maze[x-2,y] == m_wall) dir.Add("U");
      //Right
      if(maze[x,y+1] == m_wall && maze[x,y+2] == m_wall) dir.Add("R");
      //Down
      if(maze[x+1,y] == m_wall && maze[x+2,y] == m_wall) dir.Add("D");
      //Left
      if(maze[x,y-1] == m_wall && maze[x,y-2] == m_wall) dir.Add("L");

      //どこにも行けなかったら終了
      if(dir.Count == 0) break;

      //ランダムで選んだ方向へ穴を掘る
      switch(getDir(dir)){
        //Up
        case 0:
          SetPath(maze, --x, y);
          SetPath(maze, --x, y);
          break;
        //Right
        case 1:
          SetPath(maze, x, ++y);
          SetPath(maze, x, ++y);
          break;
        //Down
        case 2:
          SetPath(maze, ++x, y);
          SetPath(maze, ++x, y);
          break;
        //Left
        case 3:
          SetPath(maze, x, --y);
          SetPath(maze, x, --y);
          break;
      }
    }

    //もう行けなくなったら
    if(CDPath.Count != 0){
      int[] ret = GetPos();
      int n_x = ret[0];
      int n_y = ret[1];
      digMaze(maze,n_x,n_y);
    }
  }

  //迷路生成
  void makeMaze(){

    //maze初期化 or maze初期化test
    initMaze(maze_1);
    // initMazeTest(maze_1);   //test
    initGround(ground_1, 1, 1);

    initMaze(maze_2);
    // initMazeTest(maze_2);   //test
    initGround(ground_2, 21, 21);

    initMaze(maze_3);
    // initMazeTest(maze_3);   //test
    initGround(ground_3, -1, -1);

    //任意のmaze[,]を空にする
    maze_1[5,5] = m_empty;
    maze_2[5,5] = m_empty;
    maze_3[1,1] = m_empty;

    digMaze(maze_1, 5, 5);
    digMaze(maze_2, 5, 5);
    digMaze(maze_3, 1, 1);

    //周りを囲う
    encloseMaze(maze_1);
    encloseMaze(maze_2);
    encloseMaze(maze_3);
  }


  void createMaze(){

    //迷路生成
    makeMaze();

    //出力_maze_1
    geneMaze(maze_1, -5, 2, -5);
    geneGoal(maze_1, -4, 2, -4);
    geneGround(ground_1, -5, 2, -5);

    //出力_maze_2
    geneMaze(maze_2, -10, -42, -10);
    geneGoal(maze_2, 11, -42, 11);
    geneGround(ground_2, -10, -42, -10);

    //出力_maze_3
    geneMaze(maze_3, -15, -90, -15);
    geneGoal(maze_3, -14, -90, -14);
    geneGround(ground_3, -15, -90, -15);


  }

  void Start()
  {
    createMaze();
    UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
  }
}
