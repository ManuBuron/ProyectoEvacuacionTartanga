using UnityEngine;
using System.Collections;

public class ColTioMovil : MonoBehaviour {
	public GameObject perMovil;
	public Animator controller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.name == "Player") {
			if (Time.time >= 250f) {
				if (perMovil!=null){
					perMovil.SetActive (true);
					controller.SetFloat ("Forward", 1);
					controller.SetBool("Idle",false);
				}
			}
		}
	}
}
