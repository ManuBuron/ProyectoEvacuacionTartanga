using UnityEngine;
using System.Collections;

public class HumoAulas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("ActivarCollider",100);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void ActivarCollider(){
		if (GetComponent<BoxCollider>() != null) {
			GetComponent<BoxCollider>().enabled=true;
		}
		if (GetComponent<HumoParte>() != null) {
			GetComponent<HumoParte>().enabled=true;
		}
	}
}
