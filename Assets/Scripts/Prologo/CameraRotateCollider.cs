using UnityEngine;
using System.Collections;

public class CameraRotateCollider : MonoBehaviour {

	public GameObject CamHall;

	void OnTriggerEnter(Collider col){
		CamHall = GameObject.Find ("CameraHall");
		CamHall.GetComponent<CameraController> ().CameraRotate ();
	}

}
