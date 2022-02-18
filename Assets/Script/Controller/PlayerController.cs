using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   //プレイヤーの持つパラメータを参照
   private Vector3 pos;               //プレイヤーの座標

   //操作
   private Vector3 touchStartPos;     //画面タップ開始地点の座標
   private Vector3 touchNowPos;       //現在の座標
   private string direction;          //現在のタッチの状態を代入するstring
   private bool isTouch;              //タッチされているかどうか
   private bool isCollision = false;  //ぶつかったかどうか
   public bool OnGround = false;

   //動作
   public bool isMove;             //動いているかどうか
   [SerializeField] private float moveSpeed;  //プレイヤーの移動速度

   //SE
   private bool OnSound = false;
   public AudioClip moveSound;
   AudioSource audioSource;

   public static PlayerController instance;

   public void Awake(){

     if(instance == null){
       instance = this;
     }
   }

   //初期化
   void Start()
   {
     audioSource = GetComponent<AudioSource>();
   }

   //更新
   void Update()
   {
     //自分の位置取得
     Transform myTransform = this.transform;

     //自分の座標取得
     Vector3 myPos = myTransform.position;

     //x,z座標の調整
     if(myPos.y <= 0 && myPos.y > -40 && myPos.x >= -9.0f && myPos.z >= -9.0f){
         myPos.x -= 0.09f;
         myPos.z -= 0.09f;
         if(myPos.x < -9.0f) myPos.x = Mathf.Round(myPos.x);
         if(myPos.z < -9.0f) myPos.z = Mathf.Round(myPos.z);
         // if(myPos.x == -5.0f) moveSpeed = 0.35f;
         myTransform.position = myPos;
     }
     else if(myPos.y <= -43 && myPos.y > -85){
       if(myPos.x < 14.0f) myPos.x += 0.5f;
       if(myPos.z < 14.0f) myPos.z += 0.5f;
       if(myPos.x > 14.0f) myPos.x = Mathf.Round(myPos.x);
       if(myPos.z > 14.0f) myPos.z = Mathf.Round(myPos.z);
       myTransform.position = myPos;
     }

     if (isMove) { Move(); }//移動
     else { Flick(); }//フリックの処理
   }

   //衝突処理(Wall)
   void OnCollisionEnter(Collision collision)
   {
     if(collision.gameObject.name == "Ground"){
       OnGround = true;
     } else if(collision.gameObject.name == "OutWall"){
       INFmanager.instance.respawnPlayer();
     } else {
       isMove = false;
       isCollision = true;
     }
   }

   //フリック操作
   void Flick()
   {
       //画面がタップされた時
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
          OnSound = false;
           touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
           isTouch = true;
       }
       //画面から指が離れた時
       if (Input.GetKeyUp(KeyCode.Mouse0))
       {
           //タッチを検出
           if (direction == "touch")
           {
               //タッチの処理
               // Debug.Log("タッチ");
           }
           isTouch = false;
       }

       //現在タッチされている場合
       if (isTouch)
       {
           touchNowPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
           GetDirection();//座標からタッチ、フリックの状態を管理
       }
   }

   //座標の管理
   void GetDirection()
   {
       //現在の座標と開始地点の座標の差を代入
       float directionX = touchNowPos.x - touchStartPos.x;
       float directionY = touchNowPos.y - touchStartPos.y;

       //差の大きさによって条件分岐
       if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
       {
           if (30 < directionX)
           {
               //右向きにフリック
               direction = "right";
           }
           else if (-30 > directionX)
           {
               //左向きにフリック
               direction = "left";
           }
       }
       else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
       {
           if (30 < directionY)
           {
               //上向きにフリック
               direction = "up";
           }
           else if (-30 > directionY)
           {
               //下向きのフリック
               direction = "down";
           }
       }

       //フリック操作がなかった場合
       else
       {
           //タッチを検出
           direction = "touch";
       }

       if (direction == null || direction == "touch") { return; }

       isTouch = false;
       isMove = true;
   }

   //移動
   void Move()
   {

     //移動音を再生
     if(!OnSound){
       audioSource.PlayOneShot(moveSound);
       OnSound = true;
     }

     //Move中にFlickされたら
     if(Input.GetKeyDown(KeyCode.Mouse0)){
       isMove = false;
       Flick();
     }

     pos = transform.position;
     //フリックの方向によって分岐
     switch (direction)
     {

       case "up":
           //上フリックされた時の処理
           pos.z += moveSpeed;
           if(isCollision){
             pos.x = Mathf.Round(pos.x);
             pos.z = Mathf.Round(pos.z);
             isCollision = false;
           }
           break;

       case "down":
           //下フリックされた時の処理
           pos.z -= moveSpeed;
           if(isCollision){
             pos.x = Mathf.Round(pos.x);
             pos.z = Mathf.Round(pos.z);
             isCollision = false;
           }
           break;

       case "right":
           //右フリックされた時の処理
           pos.x += moveSpeed;
           if(isCollision){
             pos.x = Mathf.Round(pos.x);
             pos.z = Mathf.Round(pos.z);
             isCollision = false;
           }
           break;

       case "left":
           //左フリックされた時の処理
           pos.x -= moveSpeed;
           if(isCollision){
            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);
            isCollision = false;
           }
           break;
       }

       transform.position = pos;
   }
}
