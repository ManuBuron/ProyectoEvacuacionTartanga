using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlumnosFila : MonoBehaviour {

	public int ID;
	public GameObject profesor;
	public GameObject target;

	public bool moveProfesor;
	bool canMove;
	NavMeshAgent nav;

	public Transform objPadre;
	public List<Transform> alumnos = new List<Transform>();

	Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
		objPadre = transform.parent.GetComponent<Transform> ();
		if (ID != 1) {
			foreach (Transform alumno in objPadre.gameObject.GetComponentsInChildren<Transform> ()) {
				if (alumno.gameObject.tag == "Alumno") {
					alumnos.Add (alumno);
				}
			}
				
			foreach (Transform go in alumnos) {
				if (go.GetComponent<AlumnosFila> ().ID == (ID -1)) {
					target = go.gameObject;
				}
			}
		} else {
			target = profesor;
		}
		nav = GetComponent <NavMeshAgent> ();

		nav.enabled = true;

		canMove = true;
		anim.SetBool ("Idle", true);
	}

	void enableMove(){
		canMove = true;
	}

	// Update is called once per frame
	void Update () {
		moveProfesor = profesor.transform.GetComponent<Profesor> ().moverse;
		if (moveProfesor) {
			anim.SetBool ("Idle", false);
			anim.SetFloat ("Forward", 0.5f);
			float distance = Vector3.Distance (target.transform.position, transform.position);
			if (distance > 1.5) {
				if (canMove) {
					nav.enabled = true;
					nav.destination = target.transform.position;
				}
			} else if (distance < 1) {
				if (nav.enabled) {
					nav.enabled = false;
					Invoke ("enableMove", 2);
				}

			}
		} else {
			anim.SetBool ("Idle", true);
		}
	}
}