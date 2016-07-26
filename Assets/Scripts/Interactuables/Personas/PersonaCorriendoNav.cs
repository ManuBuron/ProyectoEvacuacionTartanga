using UnityEngine;
using System.Collections;

public class PersonaCorriendoNav : MonoBehaviour {

	float RunPersona;
	float WalkPersona;

	string name;
	string msjInteraccion;
	GameObject canvasC;

	Transform destino;

	public Transform[] Puntos;
	int puntosCount;
	int contPuntos = 0;

	public GameObject goPlayer;

	Animator anim;

	void Awake(){
		name = gameObject.GetComponent<Riesgo>().name;
		gameObject.transform.GetComponent<NavMeshAgent> ().enabled = true;
		RunPersona = 5f;
		WalkPersona = transform.GetComponent<NavMeshAgent> ().speed;
		transform.GetComponent<NavMeshAgent> ().speed= RunPersona;
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		goPlayer=GameObject.FindGameObjectWithTag ("Player");
		canvasC = GameObject.FindGameObjectWithTag ("canvas");
		puntosCount = Puntos.Length;
//		Debug.Log (puntosCount);
		if (puntosCount > 0) destino=Puntos[contPuntos];
		//GetComponent<NavMeshAgent> ().destination = destino.position;
		goPlayer.GetComponent<Player> ().rayActiveCorriendo = true;
		anim.SetBool ("Walk", true);
	}

	// Update is called once per frame
	void Update () {
		if (puntosCount > 0) {
				anim.SetBool ("Idle", false);
				anim.SetBool ("Run", true);
				GetComponent<NavMeshAgent> ().destination = destino.position;
			if (gameObject.transform.position.x == destino.position.x && gameObject.transform.position.z == destino.position.z) {
						GetComponent<NavMeshAgent> ().destination = destino.position;
				if (contPuntos == Puntos.Length-1) {
					anim.SetBool ("Idle", true);
				} else {
					contPuntos++;
					destino = Puntos [contPuntos];
				}
			}
		}
	}

	public void WalkSpeed(){
		transform.GetComponent<NavMeshAgent> ().speed= WalkPersona;
		goPlayer.GetComponent<Player> ().rayActiveCorriendo = false;
		gameObject.GetComponent<Riesgo>().resolved = true;
		canvasC.GetComponent<CanvasController> ().SimpleDialogeCanvasText (msjInteraccion);
		anim.SetBool ("Walk", true);
		anim.SetBool ("Run", false);
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.name == "TioCorriendoCollider") {
			WalkSpeed ();
			gameObject.GetComponent<Riesgo>().resolved = false;
		}
	}

	public void PonermsjInicial(){
			msjInteraccion = canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, true);
	}

	public void QuitmsjInicial(){
		canvasC.GetComponent<CanvasController>().ChangeCanvasPersons((string)name, false);
	}
}
