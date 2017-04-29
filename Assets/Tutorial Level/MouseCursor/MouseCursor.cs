using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {
	public Texture2D cursorTexture;
	public Texture2D cursorTexture2;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;


	void OnMouseEnter() {
		Cursor.SetCursor(cursorTexture2, Vector2.zero, cursorMode);
	}
	void OnMouseExit() {
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);
		}

		if (Input.GetMouseButtonUp (0)) {
		Cursor.SetCursor (cursorTexture2, Vector2.zero, cursorMode);
		}
		
	}


}
