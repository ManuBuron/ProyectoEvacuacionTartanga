using UnityEngine;
using System.Collections;

public class PersonaPrologo : MonoBehaviour {
	float RunPersona;

	Transform destino;
	float distance;
	public Transform[] Puntos;
	int puntosCount;
	int contPuntos = 0;

	Animator anim;

	void Awake(){
		gameObject.transform.GetComponent<NavMeshAgent> ().enabled = true;
		RunPersona = 3f;
		transform.GetComponent<NavMeshAgent> ().speed= RunPersona;
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		puntosCount = Puntos.Length;
		if (puntosCount > 0) destino=Puntos[contPuntos];
		anim.SetBool ("Idle", false);
		anim.SetBool ("Run", true);
//		anim.SetFloat ("Forward", 10);
		GetComponent<NavMeshAgent> ().destination = destino.position;
	}

	// Update is called once per frame
	void Update () {

		if (puntosCount > 0) {
			if (transform.parent.gameObject.activeSelf) {
				distance = Vector3.Distance (destino.position, gameObject.transform.position);
				//Debug.Log (distance);
				if (distance<1) {
					GetComponent<NavMeshAgent> ().Stop ();
					if (contPuntos == Puntos.Length - 1) {
						anim.SetBool ("Idle", true);
						anim.SetBool ("Run", false);
					} else {
						contPuntos++;
						destino = Puntos [contPuntos];
					}
				}
			}	
		}

	}

	public void Run(){
		transform.GetComponent<NavMeshAgent> ().speed= RunPersona + 3f;
	}


		/*if (puntosCount > 0) {
			if (((gameObject.transform.position.x - destino.position.x) < 0.1f) && (gameObject.transform.position.z -destino.position.z ) < 0.1f){
				Debug.Log("AlcanzadO");
				Debug.Log("ALCANZADO: Ethan X: " + gameObject.transform.position.x + "Ethan Y: " + gameObject.transform.position.y ); 
				Debug.Log("ALCANZADO: Objec X: " + destino.position.x + "Objec Y: " + destino.position.y ); 

				GetComponent<NavMeshAgent> ().Stop();// = destino.position;
				if (contPuntos == Puntos.Length-1) {
					anim.SetBool ("Idle", true);
					anim.SetBool ("Run", false);
				} else {
					contPuntos++;
					destino = Puntos [contPuntos];
				}
			}{
				Debug.Log("Ethan X: " + gameObject.transform.position.x + "Ethan Y: " + gameObject.transform.position.y ); 
				Debug.Log("Objec X: " + destino.position.x + "Objec Y: " + destino.position.y ); 

			}
		}*/

}
