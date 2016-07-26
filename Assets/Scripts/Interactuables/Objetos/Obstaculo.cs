using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obstaculo : MonoBehaviour {

	bool Interactuable;
	string name;
	GameObject canvasC;

	// Use this for initialization
	void Awake () {
		name = gameObject.GetComponent<Riesgo>().name;
	}

	void Start(){
		canvasC = GameObject.FindGameObjectWithTag ("canvas");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Interactuable) {
			if (Input.GetMouseButtonDown (0)) {
				ResolvedObject ();
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			Ponermsj ();
			Interactuable = true;
		}
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Quitmsj ();
			Interactuable = false;
		}
	}


	void ResolvedObject(){
		gameObject.GetComponent<Riesgo> ().resolved = true;
		Quitmsj ();
		gameObject.SetActive (false);
	}

	void Ponermsj(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasTextObject((string)name, true);
	}

	void Quitmsj(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasTextObject((string)name, false);
	}

}
