using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Profesor : MonoBehaviour {

	public bool moverse;
	bool Interactuable;
	public string msjInteraccion;

	string name;
	public GameObject canvasC;

	public Transform destino;
	public Transform[] Puntos;
	int puntosCount;
	int contPuntos = 0;

	Animator anim;


	void Awake(){
		name = transform.GetComponent<Riesgo> ().name;
		Interactuable = false;
		moverse = false;
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		anim.SetBool ("Idle", true);
		canvasC = GameObject.FindGameObjectWithTag ("canvas");
		puntosCount = Puntos.Length;
		if (puntosCount > 0) destino=Puntos[contPuntos];
	}

	// Update is called once per frame
	void Update () {


		if (Interactuable) {
			if (Input.GetMouseButtonDown (0)) {
				moverse = true;
				transform.GetComponent<Riesgo> ().resolved = true;
				canvasC.GetComponent<CanvasController> ().SimpleDialogeCanvasText (msjInteraccion);
				if (transform.parent.name == "ProfesorAlumnos308") {
					GameObject runner = GameObject.FindGameObjectWithTag ("Runner");
					if (runner != null) {
						runner.GetComponent<PersonaCorriendoNav> ().enabled = true;
					}
				}
				anim.SetBool ("Idle", false);
				anim.SetFloat ("Forward", 0.5f);
				Invoke ("QuitCollider", 3f);
			}
		}

		//Debug.Log (gameObject.transform.localPosition.y +" a " + destino.localPosition.y);
		if (moverse) {
			if (puntosCount > 0) {
				//GetComponent<NavMeshAgent> ().destination = destino.position;
				if (destino.position == Puntos [contPuntos].position) {
					//TODO: cambiar comprobación para que lo revise a menos de un metro.
					if ((transform.localPosition.x == destino.localPosition.x) &&(transform.localPosition.z == destino.localPosition.z)) {
						Debug.Log ("pas1");
						//GetComponent<NavMeshAgent> ().destination = destino.position;
						contPuntos++;
						if (contPuntos < puntosCount) {
							destino = Puntos [contPuntos];
						} else {
							anim.SetBool ("Idle", true);
							moverse = false;
						} 
					} 
				GetComponent<NavMeshAgent> ().destination = destino.position;
				}
			}
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !moverse) {
			Interactuable = true;
			msjInteraccion = canvasC.GetComponent<CanvasController> ().ChangeCanvasPersons ((string)name, Interactuable);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Interactuable = false;
			canvasC.GetComponent<CanvasController> ().ChangeCanvasPersons ((string)name, Interactuable);
		}
	}

	void QuitCollider(){
		//quit sphere collider
		transform.GetComponent<SphereCollider> ().enabled = false;
	}
}