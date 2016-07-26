using UnityEngine;
using System.Collections;

public class HumoTodo : MonoBehaviour {
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
	/*void OnTriggerEnter(Collider col){
		Damage(col);
	}*/

	void OnTriggerStay(Collider col){
		Damage(col);
	}
	
	void OnTriggerExit(Collider col){
		if (col.tag=="Player"){
			humo.playerPlayer.DejarHacerDaño();
			llamado=false;
		}
	}

	void Damage(Collider col){
		if (!llamado){
			if (col.tag=="Player"){
				if (!humo.fControl.agacharse){
					humo.playerPlayer.HacerDañoGrave();
					llamado=true;
				}else{
					humo.playerPlayer.HacerDañoLeve();
					llamado=true;
				}
			}
		}
	}
}
