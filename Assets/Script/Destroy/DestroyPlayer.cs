using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{

  public static DestroyPlayer instance;

  public void Awake(){

    if(instance == null){
      instance = this;
    }
  }

  public void destroyPlayer(){

    Destroy(gameObject);
  }
}
