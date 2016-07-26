using UnityEngine;
using System.Collections;

public class MostrarPuntuacion : MonoBehaviour {
	public Score score;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.name == "Player") {			
			score.MostrarPuntuacion();
		}
	}
}
