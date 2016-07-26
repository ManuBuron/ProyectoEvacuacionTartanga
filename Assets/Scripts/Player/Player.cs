using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	GameObject centerPos;

	//Rigidbody playerRigidBody;
	Camera playerCam;
	RaycastHit hit;
	Ray ray;
	public bool rayActiveCorriendo;
	public bool rayActiveDesordenada;

	GameObject goTioCorriendo;
	GameObject goTioDesordenado;

	int vida;
	public GameObject pulmon;
	public GameObject damage;
	public GameObject humoPulmon;
	//Color damageColor;
	bool bajarRojo;
	float colorPulmon;
	public AudioClip tosSuave;
	public AudioClip tosFuerte;
	AudioSource voz;
	public GameController gControl;

	void Awake () {
		rayActiveCorriendo = false;
		rayActiveDesordenada = false;
		playerCam = GetComponentInChildren<Camera> ();
	}

	void Start(){
		centerPos = GameObject.FindGameObjectWithTag ("CenterPoint");
		vida=100;
		bajarRojo=false;
		//damageColor=damage.GetComponent<Image>().color;
		voz=playerCam.GetComponent<AudioSource>();

	}

	
	// Update is called once per frame
	void Update () {
		// saber cuando debemos activar el rayo para detectar a la persona corriendo
		if (rayActiveCorriendo || rayActiveDesordenada){
			//ray = playerCam.ScreenPointToRay (Input.mousePosition);
			ray = playerCam.ScreenPointToRay (centerPos.transform.position);
			if (Physics.Raycast (ray, out hit)) {
				Transform objectHit = hit.transform;
				if (objectHit.tag == "Runner" && objectHit != null && rayActiveCorriendo) {
					goTioCorriendo = objectHit.gameObject;
					objectHit.transform.GetComponent<PersonaCorriendoNav> ().PonermsjInicial ();
					if (Input.GetMouseButtonDown (0)) {
						objectHit.transform.GetComponent<Riesgo> ().resolved = true;
						objectHit.transform.GetComponent<PersonaCorriendoNav> ().WalkSpeed ();
						rayActiveCorriendo = false;
					}
				} else if (goTioCorriendo != null && rayActiveCorriendo) {
					goTioCorriendo.transform.GetComponent<PersonaCorriendoNav> ().QuitmsjInicial ();
				}
			

				if (objectHit.tag == "PersonaDesordenada" && objectHit != null) {
					goTioDesordenado = objectHit.gameObject;
					objectHit.transform.GetComponent<PersonaDesordenFila> ().PonermsjInicial ();
					if (Input.GetMouseButtonDown (0)) {
						rayActiveDesordenada = false;
						objectHit.transform.GetComponent<PersonaDesordenFila> ().MoveCorrectPosition();
					}
				} else if (goTioDesordenado != null && rayActiveDesordenada) {
					goTioDesordenado.transform.GetComponent<PersonaDesordenFila> ().QuitmsjInicial ();
				}
			}
			Debug.DrawRay(playerCam.transform.position, ray.direction* 100f, Color.red);
		}
		//Debug.Log (damage.GetComponent<Image>().color.a);



		if (bajarRojo){
			if (damage.GetComponent<Image>().color.a>0.3f)
				damage.GetComponent<Image>().color=new Color(1,1,1,Mathf.Lerp(damage.GetComponent<Image>().color.a,0f,Time.deltaTime*0.2f));
			else{
				damage.SetActive (false);
				bajarRojo=true;
			}
		}
	}

	public void HacerDañoGrave(){
		if (vida == 100) {
			pulmon.SetActive (true);
			humoPulmon.SetActive (true);
		}
		vida-=10;
		Rojo ();
		Toser ();
		Invoke ("HacerDañoGrave",1);
		if (vida <= 0) {
			Muerte ();
		}
	}

	public void HacerDañoLeve(){
		if (vida == 100) {
			pulmon.SetActive (true);
			humoPulmon.SetActive (true);
		}
		vida-=5;
		Rojo ();
		Toser ();
		Invoke ("HacerDañoLeve",1);
		if (vida <= 0) {
			Muerte ();
		}		
	}
	 
	public void DejarHacerDaño(){
		CancelInvoke();
	}


	void Rojo(){
		damage.SetActive (true);
		damage.GetComponent<Image>().color=new Color(1,1,1,1);
		bajarRojo=true;

		colorPulmon=(float)(1f-(vida/100f));
		Debug.Log("Alfa: "+colorPulmon);
		//pulmon.GetComponent<Image>().color=new Color(colorPulmon,colorPulmon,colorPulmon,1);
		humoPulmon.GetComponent<Image>().color=new Color(1,1,1,colorPulmon);
	}

	void Toser(){
		if (!voz.isPlaying) {
			if (vida > 50) {
				voz.clip = tosSuave;
				voz.Play ();
				Debug.Log ("tos");
			} else {
				voz.clip = tosFuerte;
				voz.Play ();
			}
		}
	}

	void Muerte(){
		gControl.OscurecerEscena ();
	}
}