using UnityEngine;
using System.Collections;


public class HumoParte : MonoBehaviour {
	Humo humo;
	bool llamado;


	// Use this for initialization
	void Start () {
		humo=GameObject.Find ("Humo").GetComponent<Humo>();

		llamado=false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if (col.tag=="Player"){
			if (!humo.fControl.agacharse){
				if (!llamado){
					humo.playerPlayer.HacerDañoLeve();
					llamado=true;
				}
			}else{
				humo.playerPlayer.DejarHacerDaño();
				llamado=false;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag=="Player"){
			humo.playerPlayer.DejarHacerDaño();
			llamado=false;
		}
	}

}
