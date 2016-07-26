using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloorSituation : MonoBehaviour {
	Transform text;
	Text textFloor;
	string idioma;
	GameObject planta;
	public string nombre;

	// Use this for initialization
	void Start () {

		textFloor=GameObject.Find("TextCollider").GetComponent<Text>();
		//Debug.Log (textFloor.name);
		idioma = PlayerPrefs.GetString ("Idioma");
		Debug.Log (idioma);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		nombre=gameObject.tag;
		//Debug.Log (nombre);
		if (col.tag == "Player") {
			switch (idioma) {
			case "ES": 
				textFloor.text = "Estas en la planta " + nombre + ", tienes que ir a la 3ª planta.";	
				break;
			case "EN":
				textFloor.text = "You're on floor " + nombre + ", you must go to floor 3."; 	
				break;
			case "EUS":
				textFloor.text = nombre + ". solairuan zaude, 3. solairura igo behar zara."; 	
				break;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Invoke ("ClearText", 3);
		}
	}

	void ClearText(){
		textFloor.text="";
	}
}
