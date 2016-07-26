using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class PersonaMovil : MonoBehaviour {

	public Transform player;
	NavMeshAgent nav;

	bool Interactuable;
	bool Interactuado;

	//public Transform ViewPoint;

	string name;
	GameObject canvasC;

	public Transform p1;
	public Transform p2;

	bool seguir;
	GameObject centerPoint;
	Animator controller;
	MouseLook mouseLook;
	Riesgo riesgo;
	bool movilcogido;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();
		centerPoint = GameObject.Find ("CenterPoint");
	}

	void Start(){
		canvasC = GameObject.FindGameObjectWithTag("canvas");
		name = gameObject.GetComponent<Riesgo>().name;
		seguir = true;
		controller = gameObject.GetComponent<Animator> ();
		controller.SetFloat ("Forward", 1);
		controller.SetBool("Idle",false);
		mouseLook=GameObject.FindWithTag ("Player").GetComponent<FirstPersonController>().m_MouseLook;
		riesgo = gameObject.GetComponent<Riesgo> ();
		movilcogido = false;
		transform.parent.gameObject.SetActive (false);
		GameObject.Find ("TioMovilCollider").GetComponent<ColTioMovil> ().perMovil = transform.parent.gameObject;
		GameObject.Find ("TioMovilCollider").GetComponent<ColTioMovil> ().controller = controller;
	}

	void Update ()
	{
		//Debug.Log ("Puntero: " + Cursor.visible);
		if (!Interactuable && !Interactuado) {
			nav.SetDestination (player.position);
		} else {
			if (Interactuable) {				
				//player.LookAt (transform.position);
				bool clik = canvasC.GetComponent<CanvasController> ().GetClikado ();
				if (clik) {
					Quitardialogo ();
					Interactuado = true;
				}else{
					//Debug.Log ("seguir: " + seguir);
					if (seguir) {
						nav.SetDestination (player.position);
					}
				}
			}
			//Interactuado = true;
		}
			
		if (Interactuado && !Interactuable) {
			if (riesgo.resolved){
				nav.SetDestination (p1.position);
				if (gameObject.transform.position == p1.position) {
					nav.Stop();
					//this.transform.parent.gameObject.SetActive (false);
				}
			}else{
				if (!movilcogido){					
					nav.SetDestination (p2.position);
				}else{
					nav.SetDestination (p1.position);
					if (gameObject.transform.position == p1.position) {
						nav.Stop();					
					}
				}
				if ((gameObject.transform.position.x == p2.position.x)&&(gameObject.transform.position.z == p2.position.z)) {
					movilcogido=true;
				}
			}
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !Interactuado) {
			Interactuable = true;
			nav.Stop ();
			PonermsjDialogo ();
			col.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().fijarCamara= true;
			col.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().viewPerson = transform;
			seguir=false;
			controller.SetFloat ("Forward", 0);
			controller.SetBool("Idle",true);
		}
	}
	void OnTriggerExit(Collider col){

		if (col.tag == "Player") {
			seguir = true;
			nav.Resume ();
			//Quitardialogo ();
			//col.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().fijarCamara= false;
			controller.SetFloat ("Forward", 1);
			controller.SetBool("Idle",false);
		}
	}

	void Quitardialogo(){
		Interactuable = false;
		nav.Resume ();
		QuitmsjDialogo ();
		player.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().fijarCamara= false;
		//Debug.Log ("NOpuntero");
		mouseLook.lockCursor=true;
		Cursor.visible=false;
		centerPoint.SetActive (true);
		getResp ();
	}

	void PonermsjDialogo(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasPersonsDialogo((string)name, true);
		//Debug.Log ("puntero");
		mouseLook.lockCursor=false;
		Cursor.visible=true;
		centerPoint.SetActive (false);
	}

	void QuitmsjDialogo(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasPersonsDialogo((string)name, false);

	}

	void getResp(){
		bool resp;
		resp= canvasC.GetComponent<CanvasController> ().GetSolucionDialogo ();
		if (resp) {
			riesgo.resolved = true;
			//ir a punto de encuentro
		} else {
			riesgo.resolved = false;
			//ir a aula
		}

	}
}
