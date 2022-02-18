using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCountTextController : MonoBehaviour
{

  void Start(){

    GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 500, 0));
    StartCoroutine(DestroyObject());
  }

  void Update(){

    StartCoroutine(DestroyObject());
  }

  private IEnumerator DestroyObject(){

    yield return new WaitForSeconds(0.5f);
    Destroy(this.gameObject);
  }

}
