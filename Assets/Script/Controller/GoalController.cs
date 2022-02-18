using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

  [SerializeField]
	[Tooltip("発生させるエフェクト(パーティクル)")]
	private ParticleSystem particle;

  public AudioClip goalSound;
  AudioSource audioSource;

  void Start(){
    audioSource = GetComponent<AudioSource>();
  }

  //衝突処理
  void OnTriggerEnter(Collider collision){

    if(collision.gameObject.tag == "Player"){

      PlayerController.instance.OnGround = false;

      //SE
      audioSource.PlayOneShot(goalSound);

      //Particle
      ParticleSystem newParticle = Instantiate(particle);
      newParticle.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+1.0f, this.transform.position.z);
      newParticle.Play();
      Destroy(newParticle.gameObject, 5.0f);

      //時間追加ポップアップ
      AddController.instance.whenGoal();

      //時間追加
      CountDownINF.instance.addCount();

      //Score追加
      ScoreController.instance.addScore();

      //Object再配置
      INFmanager.instance.initGame();
    }
  }

}
