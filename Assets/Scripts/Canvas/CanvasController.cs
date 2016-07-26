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
	string idioma;

	string idiomaSelectPath;
	bool languageChange; 

	public Text mensajeText;
	public GameObject agachado;
	public GameObject levantado;
	public Button btn1;
	public Button btn2;
	public Button btn3;
	int solucionDialogo;
	bool dialogoSolucionado;
	bool clickado;

	string objActual;

	public GameObject centerPoint;
	public GameObject mano;

	void Awake(){
		//rellenar los contenedores de datos
		LoadLanguage();
		clickado = false;

	}

	void Start(){
		//quitar botones
		ButtonDisabled();
	}

	public void ChangeCanvasTextObject (string objName, bool ponerText){
		foreach (EstructuraObjeto objeto in objetosC.objetos)
		{
			if (objeto.nombre == objName){
				objActual = objName;
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
				objActual = objName;
				if (ponerText) {
					mensajeText.text = persona.mensajeInicial;
					mensajeOK = persona.dialogo;
				} else {
						QuitCanvasText (persona.mensajeInicial, 0f);
				}
			}
		}
		return mensajeOK;
	}

	public void ChangeCanvasPersonsDialogo(string objName, bool ponerText){	
		
		foreach (EstructuraPersonaDialogo personaD in personasDC.personasD)
		{
			if (personaD.nombre == objName){
				objActual = objName;
				if (ponerText) {
					mensajeText.text = personaD.dialogo;
					btn1.GetComponentInChildren<Text>().text = personaD.solucion1;
					btn2.GetComponentInChildren<Text>().text = personaD.solucion2;
					btn3.GetComponentInChildren<Text>().text = personaD.solucion3;
					solucionDialogo = personaD.solucionCorrecta;
					ButtonEnabled ();
				} else {
					QuitCanvasText (personaD.dialogo, 0f);
					ButtonDisabled ();
				}
			}

		}
	}

	public void SimpleDialogeCanvasText(string personMsj){
		mensajeText.text = personMsj;
		QuitCanvasText (personMsj, 3f);
	}

	public void ChangePlayerState(bool State){
		if (State) {
			levantado.SetActive(true);
			agachado.SetActive (false);
		} else {
			agachado.SetActive(true);
			levantado.SetActive(false);
		}
	}

	public void QuitCanvasText (string msj, float timeSecond){
		if (mensajeText.text == msj) {
			if (timeSecond <= 0f) {
				QuitText ();
			} else
				Invoke ("QuitText", timeSecond);
		} else {
			if (languageChange) {
				QuitText ();
				languageChange = false;
			}
		}
	}

	void QuitText(){
		mensajeText.text = "";
		objActual = "";
	}



	public void LoadLanguage(){

		if (PlayerPrefs.GetString ("Idioma")==""){
			PlayerPrefs.SetString ("Idioma","ES");
			idioma = "ES";
			languageChange = false;
		}
		else
			idioma = PlayerPrefs.GetString ("Idioma");

		switch (idioma)
		{
		case "ES":
			//cambia dependiendo el idioma
			idiomaSelectPath = ESPath;
			languageChange = true;
			break;
		case "EUS":
			//cambia dependiendo el idioma
			idiomaSelectPath = EUSPath;
			languageChange = true;
			break;
		case "EN":
			//cambia dependiendo el idioma
			idiomaSelectPath = ENPath;
			languageChange = true;
			break;
		}

//		personasC =  LoadP(Path.Combine(Application.dataPath, idiomaSelectPath));
		personasC = LoadP(idiomaSelectPath);
//		personasDC =  LoadPD(Path.Combine(Application.dataPath, idiomaSelectPath));
		personasDC = LoadPD(idiomaSelectPath);
//		objetosC =  LoadObj(Path.Combine(Application.dataPath, idiomaSelectPath));
		objetosC = LoadObj(idiomaSelectPath);
		CambiarIdiomaTrasPausa ();
	}

	// Modifica el idioma si el player entra en pause
	public void CambiarIdiomaTrasPausa() {
		ChangeCanvasTextObject (objActual, true);
		ChangeCanvasPersons (objActual, true);
		ChangeCanvasPersonsDialogo (objActual, true);
	}

	public static PersonasContainer LoadP(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(PersonasContainer));
		TextAsset textAsset = (TextAsset) Resources.Load(path);  
		using(StringReader reader = new System.IO.StringReader(textAsset.text)){
			return serializer.Deserialize(reader) as PersonasContainer;
		}
	}

	public static PersonasDialogosContainer LoadPD(string path) {		
		XmlSerializer serializer = new XmlSerializer(typeof(PersonasDialogosContainer));
		TextAsset textAsset = (TextAsset) Resources.Load(path);  
		using(StringReader reader = new System.IO.StringReader(textAsset.text)){
			return serializer.Deserialize(reader) as PersonasDialogosContainer;
		}
	}

	public static ObjetosContainer LoadObj(string path)
	{		
		XmlSerializer serializer = new XmlSerializer(typeof(ObjetosContainer));
		TextAsset textAsset = (TextAsset) Resources.Load(path);  
		using(StringReader reader = new System.IO.StringReader(textAsset.text)){
			return serializer.Deserialize(reader) as ObjetosContainer;
		}
	}

	void ButtonDisabled(){
		btn1.gameObject.SetActive (false);
		btn2.gameObject.SetActive (false);
		btn3.gameObject.SetActive (false);
	}

	void ButtonEnabled(){
		btn1.gameObject.SetActive (true);
		btn2.gameObject.SetActive (true);
		btn3.gameObject.SetActive (true);
	}

	public void ButtonResp(int btnNumber){
		clickado = true;
		if (solucionDialogo == btnNumber) {
			dialogoSolucionado = true;
		} else dialogoSolucionado = false;
	}

	public bool GetSolucionDialogo(){
		return dialogoSolucionado;
	}

	public bool GetClikado(){
		return clickado;
	}

	public void PonerMano(){
		mano.SetActive (true);
		centerPoint.SetActive (false);
	}
	public void PonerCruz(){
		mano.SetActive (false);
		centerPoint.SetActive (true);
	}
}