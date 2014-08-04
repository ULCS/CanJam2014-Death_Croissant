using UnityEngine;
using System.Collections;

public class ChooseUI : MonoBehaviour {
	
	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape))
		   {
				Application.LoadLevel("Menu");  
		   } 
	
	}
		
	void OnGUI ()
	{
		GUI.skin = customSkin;
		GUI.color = Color.green;		
		GUI.Label (new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 600, 70), "Please aim at a level card");		
	}
	
}
