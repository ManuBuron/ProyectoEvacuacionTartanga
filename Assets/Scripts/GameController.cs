using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Enums;
//using System;

public class GameController : MonoBehaviour {
	public string idioma;
	public Dictionary<string,int> diccionarioObjetos;
	public List<GameObject> openWindow;                                                                                                                                                    
	public List<GameObject> riesgos; //Lista con todos los prefabs/riesgos que se instancian no estan incluidas las ventanas ni las puertas
	public Dictionary<string,List<GameObject>> diccionarioRiesgos; //Todos los riesgos ordenados por tipo
	int num;
	public List<Riesgo> riesgosInstanciados; //Riesgos en escena menos las puertas
	GameObject player;
	int rand;
	GameObject go;
	GameObject situacion;
	GameObject obj;
	public System.TimeSpan gameTime;
	public Text time;
	Transform Riesgos;
	public Texture2D flecha;
	public GameObject negro;
	bool aclarar;
	bool oscurecer;
	public Score score;
	float tiempoInicial;
	bool tiempoRojo;
	float angulo;
	Text msgInicio;

	// Use this for initialization
	void Awake () {
		player=GameObject.FindGameObjectWithTag("Player");
		Riesgos=GameObject.Find("Riesgos").transform;
		openWindow = new List<GameObject> ();
		msgInicio = GameObject.Find("TextCollider").GetComponent<Text>();

		//MANUTO
		diccionarioObjetos = new Dictionary<string, int> ();
		diccionarioObjetos.Add("Ventanas",0);
		diccionarioObjetos.Add("Puertas",0);
		diccionarioObjetos.Add("Aulas",0);
		diccionarioObjetos.Add("Aseos",0);
		diccionarioRiesgos = new Dictionary<string, List<GameObject>> ();
		Cursor.SetCursor (flecha, Vector2.zero, CursorMode.Auto);
	

		FilledWindownList ();
		PonerVentanas ();
		SpawnRiesgos ();
		FilledListObjInstantiate ();

		aclarar = true;
		oscurecer = false;

		tiempoInicial = Time.time;
		PonerMsgInicio ();

	}
	
	// Update is called once per frame
	void Update () {
		GameClock ();

		if (aclarar){
			if (negro.GetComponent<Image>().color.a>0.2f)
				negro.GetComponent<Image>().color=new Color(0,0,0,Mathf.Lerp(negro.GetComponent<Image>().color.a,0f,Time.deltaTime*0.2f));
			else{
				negro.SetActive (false);
				aclarar=false;
			}
		}

		if (oscurecer){
			if (negro.GetComponent<Image>().color.a<0.9f)
				negro.GetComponent<Image>().color=new Color(0,0,0,Mathf.Lerp(negro.GetComponent<Image>().color.a,1f,Time.deltaTime*0.2f));
			else{				
				oscurecer=false;
				score.MostrarPuntuacion ();
			}
		}
	}

	void FilledWindownList(){
		//AddRange lista con todas las ventanas
		//Les he dado diferente tag a las ventanas de la parte izquierda pq la manilla estaba rotada al reves
		openWindow.AddRange(GameObject.FindGameObjectsWithTag("ventana"));
		openWindow.AddRange(GameObject.FindGameObjectsWithTag("ventanaLeft"));
		//Debug.Log ("Ventanas abiertasTodas " + openWindow.Count);

	}

	void PonerVentanas(){
		//Pone aleatoriamente las ventanas abiertas
		for(int i=0;i<5;i++){
			rand = Random.Range (0, openWindow.Count);
			openWindow[rand].transform.SetParent(Riesgos);
			openWindow[rand].GetComponent<VentanaController>().abierta=true;

			Transform manilla = openWindow[rand].transform.GetChild(0);
			if (openWindow [rand].tag == "ventanaLeft") {
				manilla.localRotation = Quaternion.Euler (new Vector3 (manilla.localRotation.x, manilla.localRotation.y, 90));
				angulo=Random.Range(270,330);
			} else {
				manilla.localRotation = Quaternion.Euler(new Vector3 (manilla.localRotation.x, manilla.localRotation.y, 270));
				angulo=Random.Range(30,90);
			}
			openWindow[rand].transform.rotation=Quaternion.Euler(0f,angulo,0f);
			openWindow.RemoveAt(rand);
		}
	}
	
	void SpawnRiesgos(){
		//	int num = System.Enum.GetValues (typeof(Riesgo.typeEnum)).Length; 
		//Array contiene cada tipo de riesgo:leve,moderado,grave
		string[] num = System.Enum.GetNames (typeof(TipoRiesgo));
		//Recorro array de num y riesgos y comparo el tipo de riesgo, si es igual lo guardo en una lista auxiliar. Luego añado la lista al diccionario con el indice que le correspponda
		foreach (string riskName in num) {
			List<GameObject> riesgosAux= new List<GameObject>();
			foreach(GameObject go in riesgos){
				Riesgo risk=go.GetComponentInChildren<Riesgo>();
				if(riskName== risk.type.ToString()){
					riesgosAux.Add(go);
				}
			}
			diccionarioRiesgos.Add(riskName,riesgosAux);
		}

		//Bucle para q saque 2 riesgos aleatorios de cada tipo
		for (int i=0; i<2; i++) {
			foreach (string riskName in num) { 
				List<GameObject> riesgos ;
				diccionarioRiesgos.TryGetValue(riskName,out riesgos);
				if(riesgos.Count!=0){
					rand =Random.Range(0,riesgos.Count);
					obj=riesgos[rand];
					GameObject prefab = Instantiate(obj);
					prefab.transform.SetParent(Riesgos);
					//Guardo los riesgos instanciados en una lista simple
					//riesgosInstanciados.Add(obj); * Cambio: Lo hago en FilledListObjInstantiate()
					//Debug.Log("tipos "+riskName+" riesgo: "+ obj.name);
					riesgos.RemoveAt(rand);
					//Debug.Log("riesgos "+riskName+" totales: "+riesgos.Count);
				}
			}
		}
	}


	public void FilledListObjInstantiate(){
		//Debug.Log ("Riesgos " + Riesgos.childCount);
		riesgosInstanciados= new List<Riesgo>();
		//Coje todos los elementos hijo de Riesgos y los mete en riesgosInstanciados, los que no estan activos también(true)
		Riesgos.GetComponentsInChildren<Riesgo>(true,riesgosInstanciados);
		//Debug.Log("Riesgos Instanciados " + riesgosInstanciados.Count);

		foreach (Riesgo go in riesgosInstanciados) {
			//Haces un find pero solo en la lista y devuelve un bool si no le asignas nada.Despues le asignas al go el padre que has buscado
			bool encontrado=go.transform.Find("Character001");
			//go.transform.parent = 
			if (encontrado) {
				Debug.Log ("Encontrado");
				//cubo.transform.parent=go.transform.Find("Character001");
			}
		}
	}

	
	
	void GameClock(){
		gameTime = new System.TimeSpan (0, 0, (int)(Time.time-tiempoInicial));
		time.text = "" + gameTime.Minutes + ":" + gameTime.Seconds; 
		if (!tiempoRojo) {
			if (gameTime.Minutes >= 5) {
				time.color = new Color (1, 0, 0, 1);
				tiempoRojo = true;
			}
		}
	}

	public void OscurecerEscena(){
		negro.SetActive (true);
		oscurecer = true;
	}
	
	void PonerMsgInicio(){
		msgInicio.text = "Tienes que ir a la 3ª planta.";
		Invoke ("QuitarMsgInicio", 5);
	}

	void QuitarMsgInicio(){
		msgInicio.text = "";
	}
}
