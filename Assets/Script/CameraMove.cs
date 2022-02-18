using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

  public GameObject target;
  private Vector3 distance;

    void Start()
    {

      //playerとの距離
      distance = transform.position - target.transform.position;

    }

    void LateUpdate()
    {

      //自分の位置取得
      Transform myTransform = this.transform;

      //Targetの位置取得
      Transform tarTransform = target.transform;

      //自分の座標取得
      Vector3 myPos = myTransform.position;
      // Vector3 myRot = myTransform.localEulerAngles;

      //Targetの座標取得
      Vector3 tarPos = tarTransform.position;

      // ワールド座標を基準に、回転を取得
      Vector3 worldAngle = myTransform.eulerAngles;

      if(tarPos.y > 0){
        myPos.x = 5f;
        myPos.y = tarPos.y + distance.y + 6f;
        myPos.z = -15f;
        myTransform.position = myPos;
      }
      else if(tarPos.y <= 0 && tarPos.y > -40) {
        if(myPos.x < 7.7f) myPos.x += 0.02f;
        if(myPos.y > -6.0f) myPos.y -= 0.4f;
        if(myPos.z > -22.0f) myPos.z -= 0.05f;
        if(gameObject.transform.localEulerAngles.x <= 55) transform.Rotate(new Vector3(0.1f, 0, 0));
        myTransform.position = myPos;
      }
      else if(tarPos.y <= -43 && tarPos.y > -85){
        if(myPos.x > 5.6f) myPos.x -= 0.05f;
        if(myPos.y > -40.0f) myPos.y -= 0.4f;
        if(worldAngle.x < 67f) worldAngle.x += 0.2f;
        // myPos.z -= 0.1f;
        myTransform.position = myPos;
        myTransform.eulerAngles = worldAngle;
      }
    }
}
