/*using UnityEngine;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	PersonasContainer personasC;
	PersonasDialogosContainer personasDC;
	ObjetosContainer objetosC;

	string ESPath = "XML/ES";
	string EUSPath = "XML/EUS";
	string ENPath = "XML/EN";

	string idiomaSelectPath;

	public Text mensajeText;
	public Image playerState;
	public Image imagenEstado;
	public Sprite agachado;
	public Sprite levantado;

	void Awake(){
		Debug.Log ("Path" + EUSPath);
		//cambia dependiendo el idioma
		idiomaSelectPath = EUSPath;

		//rellenar los contenedores de datos
		personasC =  LoadP(Path.Combine(Application.dataPath, idiomaSelectPath));
		personasDC =  LoadPD(Path.Combine(Application.dataPath, idiomaSelectPath));
		objetosC =  LoadObj(Path.Combine(Application.dataPath, idiomaSelectPath));
	}

	public void ChangeCanvasTextObject (string objName){
		foreach (EstructuraObjeto objeto in objetosC.objetos)
		{
			Debug.Log ("objeto " + objeto.nombre);
			if (objeto.nombre == objName){
				mensajeText.text = objeto.mensajeInicial;
			}
		}
	}
		

	public void ChangePlayerState(bool State){
		if (State) {
			imagenEstado.sprite = levantado;
		} else {
			imagenEstado.sprite = agachado;
		}
	}

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	

	}
		
	public static PersonasContainer LoadP(string path)
	{		
		var serializer = new XmlSerializer(typeof(PersonasContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as PersonasContainer;
		}
	}

	public static PersonasDialogosContainer LoadPD(string path)
	{		
		var serializer = new XmlSerializer(typeof(PersonasDialogosContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as PersonasDialogosContainer;
		}
	}

	public static ObjetosContainer LoadObj(string path)
	{		
		Debug.Log ("Ruta" + path);
		var serializer = new XmlSerializer(typeof(ObjetosContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as ObjetosContainer;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	PersonasContainer personasC;
	PersonasDialogosContainer personasDC;
	ObjetosContainer objetosC;

	string ESPath = "XML/ES";
	string EUSPath = "XML/EUS";
	string ENPath = "XML/EN";

	string idiomaSelectPath;

	public Text mensajeText;
	public Image playerState;
	public Image imagenEstado;
	public Sprite agachado;
	public Sprite levantado;

	void Awake(){
		//cambia dependiendo el idioma
		idiomaSelectPath = ESPath;

		//rellenar los contenedores de datos
		LoadLanguage();
	}

	public void ChangeCanvasTextObject (string objName, bool ponerText){
		foreach (EstructuraObjeto objeto in objetosC.objetos)
		{
			if (objeto.nombre == objName){
				if (ponerText) {
					mensajeText.text = objeto.mensajeInicial;
				} else
					QuitCanvasText (objeto.mensajeInicial, 0f);
			}
		}
	}

	public string ChangeCanvasPersons(string objName, bool ponerText){
		string mensajeOK="";
		foreach (EstructuraPersona persona in personasC.personas)
		{
			if (persona.nombre == objName){
				if (ponerText) {
					mensajeText.text = persona.mensajeInicial;
					mensajeOK = persona.dialogo;
				} else QuitCanvasText (persona.mensajeInicial, 0f);
			}
		}
		return mensajeOK;
	}

	public void SimpleDialogeCanvasText(string personMsj){
		mensajeText.text = personMsj;
		QuitCanvasText (personMsj, 3f);
	}

	public void ChangePlayerState(bool State){
		if (State) {
			imagenEstado.sprite = levantado;
		} else {
			imagenEstado.sprite = agachado;
		}
	}

	public void QuitCanvasText (string msj, float timeSecond){
		if (mensajeText.text == msj) {
			if (timeSecond <= 0f) {
				QuitText ();
			} else Invoke ("QuitText", timeSecond);
		}
	}

	void QuitText(){
		mensajeText.text = " ";
	}



	void LoadLanguage(){
		personasC =  LoadP(Path.Combine(Application.dataPath, idiomaSelectPath));
		personasDC =  LoadPD(Path.Combine(Application.dataPath, idiomaSelectPath));
		objetosC =  LoadObj(Path.Combine(Application.dataPath, idiomaSelectPath));
		foreach (EstructuraObjeto objeto in objetosC.objetos) {
			Debug.Log (objeto.nombre);
		}
	}

	public static PersonasContainer LoadP(string path)
	{
		Debug.Log("ruta"+ path);
		var serializer = new XmlSerializer(typeof(PersonasContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as PersonasContainer;
		}

	}

	public static PersonasDialogosContainer LoadPD(string path)
	{		
		var serializer = new XmlSerializer(typeof(PersonasDialogosContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as PersonasDialogosContainer;
		}
	}

	public static ObjetosContainer LoadObj(string path)
	{		
		var serializer = new XmlSerializer(typeof(ObjetosContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as ObjetosContainer;
		}
	}


}*/