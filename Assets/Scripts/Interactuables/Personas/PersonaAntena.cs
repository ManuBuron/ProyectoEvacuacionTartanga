using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersonaAntena : MonoBehaviour {
	bool moverse;
	bool Interactuable;
	public string msjInteraccion;
	Animator anim;

	string name;
	GameObject canvasC;

	public Transform destino;

	public Transform[] Puntos;
	int puntosCount;
	int contPuntos = 0;


	void Awake(){
		name = gameObject.GetComponent<Riesgo>().name;
		gameObject.transform.GetComponent<NavMeshAgent> ().enabled = false;
		Interactuable = false;
		moverse = false;
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		canvasC = GameObject.FindGameObjectWithTag ("canvas");
		puntosCount = Puntos.Length;
		if (puntosCount > 0) destino=Puntos[contPuntos];
	}

	// Update is called once per frame
	void Update () {
		if (Interactuable) {
			if (Input.GetMouseButtonDown (0)) {
				moverse = true;
				gameObject.transform.GetComponent<NavMeshAgent> ().enabled = true;
				canvasC.GetComponent<CanvasController> ().SimpleDialogeCanvasText (msjInteraccion);
				gameObject.GetComponent<Riesgo>().resolved = true;
			}
		}

		if (moverse) {
			if (puntosCount > 0 ) {
				anim.SetFloat ("Forward", 0.5f);
				Invoke ("QuitCollider", 3f);
				GetComponent<NavMeshAgent> ().destination = destino.position;
			//	if (destino.position == Puntos [contPuntos].position) {
					//if (gameObject.transform.position == destino.position) {
					if (gameObject.transform.position.x == destino.position.x && gameObject.transform.position.z == destino.position.z) {
						GetComponent<NavMeshAgent> ().destination = destino.position;
						if (contPuntos == Puntos.Length-1) {
						anim.SetBool ("Idle", true);
						} else {
							contPuntos++;
							destino = Puntos [contPuntos];
						}
					}
			//	}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !moverse) {
			Interactuable = true;
			PonermsjInicial ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Interactuable = false;
			QuitmsjInicial ();
		}
	}

	void PonermsjInicial(){
		msjInteraccion = canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, true);
	}

	void QuitmsjInicial(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, false);
	}


	void QuitCollider(){
		//quit sphere collider
		transform.GetComponent<SphereCollider> ().enabled = false;
	}

}
