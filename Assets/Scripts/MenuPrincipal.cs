using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class MenuPrincipal : MonoBehaviour {
	MensajeIdiomaContainer mensajesIdioma;
	public GameObject protocolo;
	public GameObject controles;
	public GameObject creditos;
	string idioma;
	public GameObject menuExit;
	public Texture2D flecha;

	void Start () {
		//Esp();

		CargarIdioma();
		CargarBotones();
		protocolo.SetActive (false);
		controles.SetActive (false);
		creditos.SetActive (false);
		Cursor.SetCursor (flecha, Vector2.zero, CursorMode.Auto);
	}

	// Update is called once per frame
	void Update () {

	}

	public void Esp(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ESMenu"));
		PlayerPrefs.SetString("Idioma","ES");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("ES");
	}
	public void Eng(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ENMenu"));
		PlayerPrefs.SetString("Idioma","EN");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("EN");
	}
	public void Eusk(){
		//mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/EUSMenu"));
		PlayerPrefs.SetString("Idioma","EUS");
		CargarIdioma();
		CargarBotones();
		//CambiarIdioma("EUS");
	}

	void CargarBotones(){
		foreach (Transform child in transform)
		{
			for (int i=0;i<mensajesIdioma.mensaje.Count;i++){
				if (mensajesIdioma.mensaje[i].nombre==child.name){
					child.GetComponentInChildren<Text>().text=mensajesIdioma.mensaje[i].mensaje;
				}
			}
		}
	}

	public void Jugar(){
		Application.LoadLevel("Prologo");

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

	public void Creditos(){
		creditos.SetActive (true);
		for (int i=0;i<mensajesIdioma.mensaje.Count;i++){
			if (mensajesIdioma.mensaje[i].nombre=="Creditos"){
				creditos.GetComponentInChildren<Text>().text=mensajesIdioma.mensaje[i].mensaje;
			}
		}
	}

	public void Volver (){
		protocolo.SetActive (false);
		controles.SetActive (false);
		creditos.SetActive (false);
	}

	public static MensajeIdiomaContainer CargarMensajes(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(MensajeIdiomaContainer));
		TextAsset textAsset = (TextAsset) Resources.Load(path);  
		using(StringReader reader = new System.IO.StringReader(textAsset.text)){
			return serializer.Deserialize(reader) as MensajeIdiomaContainer;
		}

		/*var serializer = new XmlSerializer(typeof(MensajeIdiomaContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as MensajeIdiomaContainer;
		}*/

	}

	/*public void CambiarIdioma(string lengua)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/Language.dat", FileMode.OpenOrCreate);
		Idioma data = new Idioma();
		data.idioma = lengua;
		bf.Serialize(file, data);
		file.Close();
	}*/

	public void CargarIdioma()
	{	
		if (PlayerPrefs.GetString ("Idioma")==""){
			PlayerPrefs.SetString ("Idioma","ES");
			idioma = "ES";
		}
		else
			idioma = PlayerPrefs.GetString ("Idioma");

		Debug.Log(idioma);

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
		/*if (File.Exists(Application.persistentDataPath + "/Language.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Language.dat", FileMode.Open);
			//Debug.Log (Application.persistentDataPath);
			Idioma data = (Idioma)bf.Deserialize(file);
			Debug.Log ("pasa" + data.idioma);
			switch (data.idioma)
			{
			case "ES":
				mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ESMenu"));
				break;
			case "EUS":
				mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/EUSMenu"));
				break;
			case "EN":
				mensajesIdioma = CargarMensajes(Path.Combine(Application.dataPath, "XML/ENMenu"));
				break;
			}
			file.Close();
		}
		else {
			CambiarIdioma("ES");
		}*/

	}

	/*[Serializable]
	class Idioma
	{
		public string idioma;
	}*/

	public void Exit(){
		menuExit.SetActive (true);
	}

	public void AceptarExit(){		
		Application.Quit();
	}

	public void CancelarExit(){
		menuExit.SetActive (false);
	}

}
