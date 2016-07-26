using UnityEngine;
using System.Collections;
using System;

public class PrologoController : MonoBehaviour {
	GameObject cameraExplosion;
	GameObject cameraHall;
	public GameObject persExplosion;
	public GameObject persHall;
	public GameObject blackImage;

	TimeSpan gameTime;
	int seconds;
	public GameObject next;
	public AudioSource alarma;
	public Texture2D flecha;

	// Use this for initialization
	void Awake () {
		cameraExplosion = GameObject.Find ("CameraExplosion");
		cameraHall = GameObject.Find ("CameraHall");
		blackImage = GameObject.Find ("BlackImage");

		seconds =8;
		gameTime = new TimeSpan(0,0,(int)Time.time);
		CameraSwitch (cameraExplosion);
		DontDestroyOnLoad (alarma.gameObject);
		Cursor.SetCursor (flecha, Vector2.zero, CursorMode.Auto);
	}

	void Update () {
		PrologoClock ();

		if (gameTime.TotalSeconds == seconds) {
			CameraSwitch (cameraHall);
		} else if (gameTime.TotalSeconds == (6)){
			blackImage.GetComponent<Animator> ().SetBool ("ChangeCam", true);
		}

		if (gameTime.TotalSeconds == 14){			
			Oscurecer ();
		}
	}

	void CameraSwitch(GameObject cam){
		if (cam == cameraExplosion) {
			cam.SetActive (true);
			persExplosion.SetActive (true);
			cameraHall.SetActive (false);
			persHall.SetActive (false);
		} else {
			cam.SetActive  (true);
			persHall.SetActive (true);
			cameraExplosion.SetActive (false);
			persExplosion.SetActive (false);

		}
	}

	public void PrologoClock(){
		gameTime = new TimeSpan (0, 0, (int)Time.time);
	}
		
	public void Play(){
		Application.LoadLevel ("Level");
	}

	public void Oscurecer(){
		blackImage.GetComponent<Animator> ().SetBool ("blackScreen", true);
		Invoke ("Play", 1f);
		next.SetActive (false);
		alarma.Play();
	}
}
