using UnityEngine;
using System.Collections;

public class IncluirPuertas : MonoBehaviour {

	GameController gameControl;


	// Use this for initialization
	void Start () {
		gameControl=GameObject.Find ("GameController").GetComponent<GameController>();
		foreach (Transform transHijo in gameObject.transform) {
			gameControl.riesgosInstanciados.Add(transHijo.GetComponent<Riesgo>());
			Debug.Log (transHijo.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
