using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class XMLManager : MonoBehaviour {

	PersonasContainer personasC;
	PersonasDialogosContainer personasDC;
	ObjetosContainer objetosC;

	string ESPath = "XML/ES";
	string EUSPath = "XML/EUS";
	string ENPath = "XML/EN";

	string idiomaSelectPath;


	public void Start()
	{
		//cambia dependiendo el idioma
		idiomaSelectPath = ESPath;

		personasC =  LoadP(Path.Combine(Application.dataPath, idiomaSelectPath));
		foreach (EstructuraPersona persona in personasC.personas)
		{
			Debug.Log("NOMBRE PERSONA: "+ persona.nombre);
			Debug.Log("MENSAJE INICIAL: "+ persona.mensajeInicial);
			Debug.Log("MENSAJE INICIAL: "+ persona.dialogo);
			Debug.Log("--------------------------------------------------------------");
		}

		personasDC =  LoadPD(Path.Combine(Application.dataPath, idiomaSelectPath));
		/*foreach (EstructuraPersonaDialogo persona in personasDC.personasD)
		{
			Debug.Log("NOMBRE PERSONA: "+ persona.nombre);
			Debug.Log("DIALOGO: "+ persona.dialogo);
			Debug.Log("SOLUCION1: "+ persona.solucion1);
			Debug.Log("SOLUCION2: "+ persona.solucion2);
			Debug.Log("SOLUCION3: "+ persona.solucion3);
			Debug.Log("SOLUCION CORRECTA: "+ persona.solucionCorrecta);
			Debug.Log("--------------------------------------------------------------");
		}*/
			
		objetosC =  LoadObj(Path.Combine(Application.dataPath, idiomaSelectPath));
		/*foreach (EstructuraObjeto objeto in objetosC.objetos)
		{
			Debug.Log("NOMBRE PERSONA: "+objeto.nombre);
			Debug.Log("MENSAJE INICIAL: "+ objeto.mensajeInicial);
			Debug.Log("--------------------------------------------------------------");
		}*/

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
		var serializer = new XmlSerializer(typeof(ObjetosContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{	
			return serializer.Deserialize(stream) as ObjetosContainer;
		}
	}
}

