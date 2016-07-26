using UnityEngine;
using System.Collections;

public class MaxVelocidadCollider : MonoBehaviour {

	public GameObject particles1;
	public GameObject particles2;
	public GameObject particles3;

	void Start(){
		particles1.SetActive (false);
		particles2.SetActive (false);
		particles3.SetActive (false);
	}

	void OnTriggerEnter(Collider col){
		col.GetComponent<PersonaPrologo> ().Run ();
		particles1.SetActive (true);
		particles2.SetActive (true);
		particles3.SetActive (true);
	}
}
