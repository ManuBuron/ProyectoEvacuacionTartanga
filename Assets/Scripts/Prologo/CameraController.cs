using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	Animator anim;

	void Awake(){
		anim = transform.GetComponent<Animator> ();
	}

	public void CameraRotate(){
		anim.SetBool ("rotate", true);
	}
}
