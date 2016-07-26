using UnityEngine;
using System.Collections;

public class PersonaDesordenFila : MonoBehaviour {

	public Transform correctPoint;
	public Transform incorrectPoint;
	public GameObject goPlayer;

	public GameObject goDesordenadoPadre;
	public GameObject goTioDesordenado;

	string name;
	string msjInteraccion;
	GameObject canvasC;
	NavMeshAgent nav;

	Animator anim;


	bool moveCorrectPos;

	// Use this for initialization
	void Start () {
		goPlayer = GameObject.FindGameObjectWithTag ("Player");
		goDesordenadoPadre = GameObject.Find("Alum6DESORDENADO");
		incorrectPoint=goDesordenadoPadre.transform.FindChild ("PosIncorrecta");
		correctPoint=goDesordenadoPadre.transform.FindChild ("PosCorrecta");
		goTioDesordenado = transform.parent.gameObject;
		/*if (goDesordenadoPadre != null) {
			goTioDesordenado.transform.parent = goDesordenadoPadre.transform;
		} */
		canvasC = GameObject.Find ("CanvasGame");
		name = transform.GetComponent<Riesgo> ().name;
		goPlayer.GetComponent<Player> ().rayActiveDesordenada = true;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		if (goDesordenadoPadre.GetComponent<AlumnosFila>().moveProfesor){
			anim.SetFloat ("Forward", 0.5f);
		}
		if (goDesordenadoPadre.gameObject.activeInHierarchy) {
			if (moveCorrectPos) {
				nav.destination = correctPoint.position;
				transform.position = Vector3.Lerp (transform.position, correctPoint.position, 2f * Time.deltaTime);
			} else {
				nav.destination = incorrectPoint.position;
			}
		} else {
			goPlayer.GetComponent<Player> ().rayActiveDesordenada = false;
			this.transform.parent.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.name == "OrdenarTioCollider") {
			moveCorrectPos = true;
			goPlayer.GetComponent<Player> ().rayActiveDesordenada = false;
		}
	}

	public void MoveCorrectPosition(){
		//mover a la posicion correcta
		moveCorrectPos = true;
		goPlayer.GetComponent<Player> ().rayActiveDesordenada = false;
		gameObject.GetComponent<Riesgo>().resolved = true;
		canvasC.GetComponent<CanvasController> ().SimpleDialogeCanvasText (msjInteraccion);
	}

	public void PonermsjInicial(){
		msjInteraccion = canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, true);
	}

	public void QuitmsjInicial(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, false);
	}

}