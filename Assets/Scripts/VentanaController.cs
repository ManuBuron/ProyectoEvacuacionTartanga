using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VentanaController : MonoBehaviour {
	bool Interactuable; //Comprueba si estas cerca del objeto/ventana o no
	CanvasController canvasC;
	public string name;
	bool resuelto;
	Riesgo riesgo;
	public bool abierta;
	Transform ventana;
	Transform manilla;
	bool cerrar;


	void Awake () {
		riesgo = gameObject.GetComponent<Riesgo>();
		name = riesgo.name;
		//name = gameObject.GetComponent<Riesgo>().name;
		canvasC = GameObject.FindGameObjectWithTag ("canvas").GetComponent<CanvasController>();
		Interactuable = false;
		ventana = GetComponent<Transform> ();
		manilla = ventana.transform.GetChild(0);
	//	Debug.Log ("ManillaName " + manilla.name);
	}
	void Start(){
		cerrar = false;

	}

	public void Update(){
		if (Interactuable) {
			if(Input.GetMouseButtonDown (0)){
				abierta = false;
				cerrar = true;
				GetComponent<AudioSource> ().Play ();
			}
		}
		if (cerrar) {
			CloseWindow ();
		}
	}

	public void CloseWindow(){
		if (gameObject.tag == "Ventana") {
			if (transform.rotation.y > 0) {				
				if (transform.rotation.y > 0.46f) {					
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, 0.5f, 0)), Time.deltaTime * 2.5f);
				} else {
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0.5f, 0));
				}
			} else {				
				manilla.rotation = Quaternion.Lerp (manilla.transform.rotation, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * 2.5f);
			}
		} else {
			if (transform.rotation.y > 0) {
				//Debug.Log (transform.rotation.eulerAngles.y);
				if (transform.rotation.y > 0.04f) {
					//Debug.Log (transform.rotation.y);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, 0f, 0)), Time.deltaTime * 2.5f);
				} else {
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0f, 0));
				}
			} else {
				//if (manilla.rotation.z<0.7f){
				manilla.rotation = Quaternion.Lerp (manilla.transform.rotation, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * 2.5f);
				//}else{
				//manilla.rotation=Quaternion.Euler (new Vector3(0,0,90));
				//cerrar = false;
				//abierta=false;
				//}
			}
		}

		riesgo.resolved = true;
	}
		
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && abierta) {
				Interactuable = true;
				PonerMsj ();
			}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Interactuable = false;
			QuitMsj ();
		}
	}
		
	void PonerMsj(){
		canvasC.ChangeCanvasTextObject(name, true);
		canvasC.PonerMano ();
	}

	void QuitMsj(){
		canvasC.ChangeCanvasTextObject(name, false);
		canvasC.PonerCruz ();
	}

}
