using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AulaNumController : MonoBehaviour {

	// Array en el que guardamos todos los componentes TextMesh de los bloques de texto
	// 0 -> Numero
	// 1 -> Castellano
	// 2 -> Euskera

	// Los strings que vamos a usar para cambiar el contenido de los bloques de texto
	[TextArea(2,10)]
	public string numeroAula;
	[TextArea(2,10)]
	public string castellanoAula;
	[TextArea(2,10)]
	public string euskeraAula;
	

	private void updateEditorText()
	{
		List<TextMesh> textMeshList = this.loadTextMeshes ();

		textMeshList [0].text = this.numeroAula;
		textMeshList [1].text = this.castellanoAula;
		textMeshList [2].text = this.euskeraAula;

	}

	public void updateText( string numAula , string casAula , string eusAula )
	{
		List<TextMesh> textMeshList = this.loadTextMeshes ();
		
		textMeshList [0].text = numAula;
		textMeshList [1].text = casAula;
		textMeshList [2].text = eusAula;
		
	}

	private List<TextMesh> loadTextMeshes()
	{
		List<TextMesh> textMeshList = new List<TextMesh> ();

		Transform numeroGroup = this.transform.FindChild ("numero");

		textMeshList.Add ( numeroGroup.FindChild("aulaNum_num").GetComponent<TextMesh>() );

		Transform nombreGroup = this.transform.FindChild ("nombre");

		textMeshList.Add ( nombreGroup.FindChild ("aulaNum_cas").GetComponent<TextMesh> () );
		textMeshList.Add ( nombreGroup.FindChild ("aulaNum_eus").GetComponent<TextMesh> () );

		return textMeshList;
	}

 	//Sirve para hacer pequeñas pruebas en el editor con el bloque de texto :)
	void OnValidate()
	{

		this.updateEditorText ();

	}



}
