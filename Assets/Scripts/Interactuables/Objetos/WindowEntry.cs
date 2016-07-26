using UnityEngine;
using System.Collections;

public class WindowEntry : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "AntenaMan") {
			anim = col.gameObject.GetComponent<Animator> ();
			col.gameObject.GetComponent<NavMeshAgent> ().height = 0.9f;
			col.gameObject.GetComponent<CapsuleCollider> ().height = 0.9f;
			anim.SetBool ("Crouch", true);
			col.gameObject.GetComponent<NavMeshAgent> ().speed=col.gameObject.GetComponent<NavMeshAgent> ().speed/4;

		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "AntenaMan") {
			anim = col.gameObject.GetComponent<Animator> ();
			anim.SetBool ("Crouch", false);
			col.gameObject.GetComponent<NavMeshAgent> ().speed=col.gameObject.GetComponent<NavMeshAgent> ().speed*4;
		}
	}
}
