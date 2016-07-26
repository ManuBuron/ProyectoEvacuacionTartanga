using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CerrarVentana : MonoBehaviour {
	GameObject ui;
	// Use this for initialization
	void Start () {
		ui=GameObject.Find("Canvas");
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		Debug.Log (col.tag);
		if (col.tag == "Player") {
			ui.GetComponentInChildren<Text>().text = "Cerrar la ventana";
		}
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			BorrarCanvas();
		}
	} 


	void BorrarCanvas(){
		ui.GetComponentInChildren<Text>().text = "";
	}

}
