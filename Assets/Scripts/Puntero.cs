using UnityEngine;
using System.Collections;

public class Puntero : MonoBehaviour {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	Screen pantalla;

	void Start(){



		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		Cursor.visible=true;
		//Cursor.lockState=CursorLockMode.Confined;

	}


}
