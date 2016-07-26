using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuPause : MonoBehaviour {
	string idioma;
	public Text botonReanudar;
	public Text botonProtocolo;
	public Text botonControles;
	public GameObject pause;
	public GameObject protocolo;
	public GameObject controles;
	MensajeIdiomaContainer mensajesIdioma;
	public GameObject Player;
	MouseLook mouseLook;
	bool pausado;
	public GameObject menuExit;

	// Use this for initialization
	void Start () {
		CargarIdioma();
		CargarBotones();
		pausado=false;
		protocolo.SetActive (false);
		controles.SetActive (false);
		pause.SetActive(false);
		mouseLook=GameObject.FindWithTag ("Player").GetComponent<FirstPersonController>().m_MouseLook;
		Cursor.visible=false;
	}
	
	// Update is called once per frame
	void Update () {
		//leer teclado
		if (Input.GetKeyDown (KeyCode.Escape)){
			//if (pausado)
			//	Reanudar();
			//else
				Pause();
		}

		if (Input.GetKeyDown(KeyCode.F3)){			
			mouseLook.lockCursor=false;
			Cursor.visible=true;
		}
	}

	public void Pause(){
		pausado=true;
		Time.timeScale=0f;
		pause.SetActive(true);
		mouseLook.lockCursor=false;
		Cursor.visible=true;

	}

	public void Reanudar(){
		pausado=false;
		Time.timeScale=1f;
		pause.SetActive(false);
		//mouseLook.lockCursor=true;
		Cursor.visible=false;
	}

	public void Protocolo(){
		protocolo.SetActive (true);
		for (int i=0;i<mensajesIdioma.mensaje.Count;i++){
			if (mensajesIdioma.mensaje[i].nombre=="Protocolo"){
				protocolo.GetComponentInChildren<Text>().text=mensajesIdioma.mensaje[i].mensaje;
			}
		}
	}

	public void Controles(){
		controles.SetActive (true);
		for (int i=0;i<mensajesIdioma.mensaje.Count;i++){
			if (mensajesIdioma.mensaje[i].nombre=="Controles"){
				controles.GetComponentInChildren<Text>().text=mensajesIdioma.mensaje[i].mensaje;
			}
		}
	}

	public void Esp(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ESMenu"));
		PlayerPrefs.SetString("Idioma","ES");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("ES");
		transform.GetComponent<CanvasController>().LoadLanguage();
	}
	public void Eng(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ENMenu"));
		PlayerPrefs.SetString("Idioma","EN");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("EN");
		transform.GetComponent<CanvasController>().LoadLanguage();
	}

	public void Eusk(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/EUSMenu"));
		PlayerPrefs.SetString("Idioma","EUS");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("EUS");
		transform.GetComponent<CanvasController>().LoadLanguage();
	}

	public void Volver (){
		protocolo.SetActive (false);
		controles.SetActive (false);
	}

	public static MensajeIdiomaContainer CargarMensajes(string path)
	{
		/*var serializer = new XmlSerializer(typeof(MensajeIdiomaContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MensajeIdiomaContainer;
		}*/

		XmlSerializer serializer = new XmlSerializer(typeof(MensajeIdiomaContainer));
		TextAsset textAsset = (TextAsset) Resources.Load(path);  
		using(StringReader reader = new System.IO.StringReader(textAsset.text)){
			return serializer.Deserialize(reader) as MensajeIdiomaContainer;
		}
	}

	public void CargarIdioma()
	{	
		if (PlayerPrefs.GetString ("Idioma")==""){
			PlayerPrefs.SetString ("Idioma","ES");
			idioma = "ES";
		}
		else
			idioma = PlayerPrefs.GetString ("Idioma");
		
		switch (idioma)
		{
		case "ES":
			//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ESMenu"));
			mensajesIdioma = CargarMensajes("XML/ESMenu");
			break;
		case "EUS":
			//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/EUSMenu"));
			mensajesIdioma = CargarMensajes("XML/EUSMenu");
			break;
		case "EN":
			//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ENMenu"));
			mensajesIdioma = CargarMensajes("XML/ENMenu");
			break;
		}
	}

	void CargarBotones(){
		for (int i=0;i<mensajesIdioma.mensaje.Count;i++){
			switch (mensajesIdioma.mensaje[i].nombre)
			{
			case "Protocolo":
				botonProtocolo.text=mensajesIdioma.mensaje[i].mensaje;
				break;
			case "Reanudar":
				botonReanudar.text=mensajesIdioma.mensaje[i].mensaje;
				break;
			case "Controles":
				botonControles.text=mensajesIdioma.mensaje[i].mensaje;
				break;
			}
		}
	}

	public void Exit(){
		menuExit.SetActive (true);
	}

	public void AceptarExit(){
		menuExit.SetActive (false);
		Application.LoadLevel(0);

	}

	public void CancelarExit(){
		menuExit.SetActive (false);
	}


}
