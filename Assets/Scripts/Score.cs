using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;



public class Score : MonoBehaviour {
	GameObject canvasScore;
	Dictionary<string,int> puntosgravedad;
	MensajesRiesgos mensajesRiesgo;

	int puntuacion;

	List<GameObject> XXX;//todas las X rojas

	public int tiempoTotal;
	int puntosTiempo;
	public int tiempoCorrido;


	string textoFallos;
	string textoPor;
	Text textoCanvas;
	Text porCanvas;
	Text textoTotal;
	int contaFallos;

	int numObjetos;
	int numTotalObjetos;

	int porcentaje;


	GameController gameControl;

	string textoError;
	int numTipoRiesgo;

	MouseLook mouseLook;

	System.TimeSpan tiempoSpan;

	void Start () {
		//Porcentaje que quita dependiendo de la gravedad
		puntosgravedad = new Dictionary<string, int> ();
		puntosgravedad.Add ("Leve",5);
		puntosgravedad.Add ("Moderado",10);
		puntosgravedad.Add ("Grave",20);



		XXX = new List<GameObject>();

		puntuacion=100;
		contaFallos=0;
		//ventanas=5;
		//puertas=18;
		//revisadas=18;

		textoCanvas=GameObject.Find ("TxtFallos").GetComponent<Text>();
		porCanvas=GameObject.Find ("TxtPuntuacion").GetComponent<Text>();
		textoTotal=GameObject.Find ("Total").GetComponent<Text>();
		//Invoke("MostrarPuntuacion",2);

		for(int i = 0; i < 10; i++){
			XXX.Add(GameObject.Find("X"+(i+1)));
			XXX[i].SetActive(false);
		}

		gameControl=GameObject.Find ("GameController").GetComponent<GameController>();

		canvasScore=GameObject.Find("Score");
		canvasScore.SetActive (false);

		mouseLook=GameObject.FindWithTag ("Player").GetComponent<FirstPersonController>().m_MouseLook;

	}

	void Update () {		
	}

	public void MostrarPuntuacion(){
		Time.timeScale = 0;
		canvasScore.SetActive (true);
		mouseLook.lockCursor=false;
		Cursor.visible=true;

		textoFallos="";
		textoPor="";

		//Riesgos
		mensajesRiesgo = CargarRiesgo(Path.Combine(Application.dataPath, "XML/MensajesError"));
		numTipoRiesgo=0;

		gameControl.riesgosInstanciados=gameControl.riesgosInstanciados.OrderBy(Riesgo=>Riesgo.name).ToList();

		Debug.Log ("riesgos instanciados Manu: " + gameControl.riesgosInstanciados.Count);
		for (int i=0; i<gameControl.riesgosInstanciados.Count; i++){
			Debug.Log (gameControl.riesgosInstanciados[i].name);
			if (!gameControl.riesgosInstanciados[i].resolved){
				numTipoRiesgo+=1;
				if ((i==gameControl.riesgosInstanciados.Count-1)||((gameControl.riesgosInstanciados[i].name)!=(gameControl.riesgosInstanciados[i+1].name))){
					for (int x=0; x<mensajesRiesgo.errors.Count; x++){
						if (mensajesRiesgo.errors[x].nombre==gameControl.riesgosInstanciados[i].name){
							contaFallos+=1;
							puntosgravedad.TryGetValue(gameControl.riesgosInstanciados[i].type.ToString(),out porcentaje);
							puntuacion-=porcentaje*numTipoRiesgo;
							if (numTipoRiesgo==1){
								textoFallos+=mensajesRiesgo.errors[x].mensaje1 + "\n";
							}else{
								textoError = mensajesRiesgo.errors[x].mensaje2.Replace("NUMERO",numTipoRiesgo.ToString());
								textoFallos+=textoError + "\n";
							}
							textoPor+="-" + porcentaje*numTipoRiesgo + "%\n";
							numTipoRiesgo=0;
						}
					}
				}
			}
		}


		//FALTA ORDEN DE LAS CLASES

		tiempoCorrido=GameObject.FindWithTag ("Player").GetComponent<FirstPersonController>().runTimeInt;
		if (tiempoCorrido>0){
			contaFallos+=1;
			if(tiempoCorrido<5){			
				porcentaje=1;			
			}else{
				porcentaje=Mathf.RoundToInt(tiempoCorrido/5);
			}
			puntuacion-=porcentaje;
			for (int i=0; i<mensajesRiesgo.errors.Count; i++){
				if (mensajesRiesgo.errors[i].nombre=="TiempoCorrido"){
					if (tiempoCorrido==1){
						textoFallos+=mensajesRiesgo.errors[i].mensaje1 + "\n";
					}else{
						textoError = mensajesRiesgo.errors[i].mensaje2.Replace("NUMERO",numTipoRiesgo.ToString());
						textoFallos+=textoError + "\n";
					}
					textoPor+="-" + porcentaje + "%\n";
				}		
			}
		}
		tiempoSpan= GameObject.Find ("GameController").GetComponent<GameController> ().gameTime;
		tiempoTotal=(int)tiempoSpan.TotalSeconds;
		if (tiempoTotal>300){
			contaFallos+=1;
			puntosTiempo=Mathf.RoundToInt((tiempoTotal-300)/5);	
			for (int i=0; i<mensajesRiesgo.errors.Count; i++){
				if (mensajesRiesgo.errors[i].nombre=="Tiempo"){
					puntuacion-=puntosTiempo;
					textoFallos+=mensajesRiesgo.errors[i].mensaje1 + "\n";			
					textoPor+="-"+ puntosTiempo +"%\n";
				}
			}
		}



		Debug.Log ("fallos= " +contaFallos);
		for(int i = 0; i < contaFallos; i++){
			//Debug.Log (i);
			XXX[i].SetActive(true);
		}
		textoCanvas.text=textoFallos;
		porCanvas.text=textoPor;

		if (puntuacion>0){
			textoTotal.text="Total:   " + puntuacion + "%" + "  en "+ tiempoSpan.Minutes + " min y " + tiempoSpan.Seconds + " seg";
			if (puntuacion<50){
				textoTotal.color=new Color(1, 0, 0, 1);
			}else if (puntuacion<90){
				textoTotal.color=new Color(1, 0.92f, 0.016f, 1);
			}else if (puntuacion>=90){
				textoTotal.color=new Color(0, 1, 0, 1);
			}
		}else{
			textoTotal.text="Total:   0%";
			textoTotal.color=new Color(1, 0, 0, 1);
		}

	}

	public static MensajesRiesgos CargarRiesgo(string path)
	{
		var serializer = new XmlSerializer(typeof(MensajesRiesgos));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MensajesRiesgos;
		}
	}


}
