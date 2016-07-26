using UnityEngine;
using System.Collections;


public class NewDoor : MonoBehaviour {

	public CanvasController canvasC;
	string name;

	bool Interactuable;
	bool open;

	Animator doorAnim;
		
	//HUMO
	Vector3 puntoInicioHumo;
	public float tiempoInicioHumo;
	bool humoIniciado;
	public bool otroLado;
	public bool wc;
	public bool aulaGrande;
	AudioSource sonido;
	public AudioClip abrirPuerta;
	public AudioClip cerrarPuerta;

	// Use this for initialization
	void Start () {
		doorAnim = transform.GetComponent<Animator> ();
		name = gameObject.GetComponent<Riesgo>().name;
		canvasC = GameObject.FindGameObjectWithTag ("canvas").GetComponent<CanvasController>();
		Interactuable = false;
		transform.GetComponent<Riesgo> ().resolved = true;
		
		//HUMO
		puntoInicioHumo=transform.position;
		humoIniciado=false;

		sonido = GetComponent<AudioSource> ();
	}

	public void Update(){
		if (open) {			
			//HUMO
			if (Time.time > tiempoInicioHumo && !humoIniciado) {
				if (wc){
					//Instantiate (Resources.Load ("Prefabs/PrefabsDefinitivos/HumoWC"), puntoInicioHumo, Quaternion.Euler (Vector3.forward));
				}else if(aulaGrande){
					if (otroLado) {
						Instantiate (Resources.Load ("Prefabs/PrefabsDefinitivos/HumoAulaGrande"), puntoInicioHumo, Quaternion.Euler (Vector3.forward + new Vector3 (0, 180, 0)));
					} else {
						Instantiate (Resources.Load ("Prefabs/PrefabsDefinitivos/HumoAulaGrande"), puntoInicioHumo, Quaternion.Euler (Vector3.forward));	
					}
				}else if (otroLado) {
					Instantiate (Resources.Load ("Prefabs/PrefabsDefinitivos/HumoAula"), puntoInicioHumo, Quaternion.Euler (Vector3.forward + new Vector3 (0, 180, 0)));
				} else {
					Instantiate (Resources.Load ("Prefabs/PrefabsDefinitivos/HumoAula"), puntoInicioHumo, Quaternion.Euler (Vector3.forward));
				}
				humoIniciado = true;
			}
		}

		if (Interactuable) {
			if (Input.GetMouseButtonDown (0)) {
				if (!open) {
					open = true;
					doorAnim.SetBool ("Open", open);
					doorAnim.SetBool ("Close", !open);
					transform.GetComponent<Riesgo> ().resolved = false;
					sonido.clip = abrirPuerta;
					sonido.Play ();

				} else {
					open = false;
					doorAnim.SetBool ("Open", open);
					doorAnim.SetBool ("Close", !open);
					transform.GetComponent<Riesgo> ().resolved = true;
					sonido.clip = cerrarPuerta;
					sonido.Play ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			Interactuable = true;
			Ponermsj ();
		} else {
			open = true;
			doorAnim.SetBool ("Open", open);
			doorAnim.SetBool ("Close", !open);
			transform.GetComponent<Riesgo> ().resolved = false;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			Interactuable = false;
			Quitmsj ();
		}
	}

	void Ponermsj(){
		canvasC.ChangeCanvasTextObject((string)name, true);
		canvasC.PonerMano ();
	}

	void Quitmsj(){
		canvasC.ChangeCanvasTextObject((string)name, false);
		canvasC.PonerCruz ();
	}
}
