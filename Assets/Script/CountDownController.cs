using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownController : MonoBehaviour
{
  [SerializeField] private Text _textCountdown;

  //SE
  public AudioClip SE_CountDown;
  public AudioClip SE_Go;
  AudioSource audioSource;

	void Start(){
		_textCountdown.text = "";
    audioSource = GetComponent<AudioSource>();
    StartCoroutine(CountdownCoroutine());
	}

	IEnumerator CountdownCoroutine(){
		_textCountdown.gameObject.SetActive(true);

		_textCountdown.text = "3";
    audioSource.PlayOneShot(SE_CountDown);
		yield return new WaitForSeconds(1.0f);

		_textCountdown.text = "2";
    audioSource.PlayOneShot(SE_CountDown);
		yield return new WaitForSeconds(1.0f);

		_textCountdown.text = "1";
    audioSource.PlayOneShot(SE_CountDown);
		yield return new WaitForSeconds(1.0f);

		_textCountdown.text = "GO!";
    audioSource.PlayOneShot(SE_Go);
		yield return new WaitForSeconds(1.0f);

		_textCountdown.text = "";
		_textCountdown.gameObject.SetActive(false);
    ChangeScene();
  }

  void ChangeScene(){
    SceneManager.LoadScene("InfGame");
  }

}
