using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{

  public static DestroyWall instance;

  public void Awake(){

    if(instance == null){
      instance = this;
    }
  }

  //Wallの削除
  public void destroyWall(){
      // Destroy(gameObject,1f);

      GameObject[] balls = GameObject.FindGameObjectsWithTag("Wall");

       foreach (GameObject wall in balls)
       {
           Destroy(wall);
       }

  }
}
