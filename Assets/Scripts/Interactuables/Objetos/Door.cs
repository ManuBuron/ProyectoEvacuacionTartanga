using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class Door : MonoBehaviour {

	public GameObject canvasC;
	string name;

	//public Transform[] manillas;
	public List<Transform> manillas = new List<Transform>();

	bool Interactuable;
	bool open;
	bool isDoorReady=false;
	bool isOpenDoor=false;


	//HUMO
	Vector3 puntoInicioHumo;
	public float tiempoInicioHumo;
	bool humoIniciado;

	// Use this for initialization
	void Start () {
		name = gameObject.GetComponent<Riesgo>().name;
		canvasC = GameObject.FindGameObjectWithTag ("canvas");
		if ((int)transform.rotation.eulerAngles.y > 0) {
			open= true;
		}else open= false;
		foreach (Transform manilla in gameObject.GetComponentsInChildren<Transform> ()) {
			if (manilla.gameObject.tag != "Puerta") {
				manillas.Add (manilla);
			}
		}
		Interactuable = false;

		//HUMO
		puntoInicioHumo=transform.position;
		humoIniciado=false;
	}
	
	public void FixedUpdate(){
		if (open) {
			foreach (Transform manilla in manillas) {
				if ((int)manilla.rotation.eulerAngles.z != 330 && !isOpenDoor) {
					manilla.localRotation = Quaternion.Lerp (manilla.localRotation, Quaternion.Euler (new Vector3 (0, 0, 330)), Time.deltaTime * 1.5f);
				} else {
					isDoorReady = true;
				}
			}

			if ((int)transform.rotation.eulerAngles.y != 270 && isDoorReady == true) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, 270, 0)), Time.deltaTime * 5f);
				foreach (Transform manilla in manillas) {
					if ((int)manilla.rotation.eulerAngles.z != 0) {
						manilla.localRotation = Quaternion.Lerp (manilla.localRotation, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * 1.5f);
						isDoorReady = false;
						isOpenDoor = true;
						transform.GetComponent<Riesgo> ().resolved = true;
					}
				}
			}
		} else {
			if ((int)transform.rotation.eulerAngles.y != 0) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * 5f);
			} else {
				isDoorReady = false;
				isOpenDoor = false;
				transform.GetComponent<Riesgo> ().resolved = false;
			}

		}

		if (Interactuable) {
			if (Input.GetMouseButtonDown (0)) {
				if (!open) {
					open = true;
					//HUMO
					if (Time.time>tiempoInicioHumo && !humoIniciado){
						Instantiate(Resources.Load ("Prefabs/PrefabsDefinitivos/HumoAula"),puntoInicioHumo,Quaternion.Euler(Vector3.forward));
						humoIniciado=true;
					}
				} else {
					open = false;
				}
			}
		}
	}
		
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			Interactuable = true;
			Ponermsj ();
		}
	}
		void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Interactuable = false;
			Quitmsj ();
		}
	}

	void Ponermsj(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasTextObject((string)name, true);
	}

	void Quitmsj(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasTextObject((string)name, false);
	}
}