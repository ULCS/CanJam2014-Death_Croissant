using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	
	public static bool Win = false;
	
	// Use this for initialization
	void Start () {
		Win = false;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)	
	{
		if(other.gameObject.tag == "Player" ) 
		{
			Win = true;
			//other.gameObject.SetActive(false);
			//other.GetComponent(MeshRenderer).enabled = false;
		}
	}
	
	void OnGUI ()
	{
		//GUI.skin = customSkin;		
		//GUI.Label (new Rect (Screen.width - 220,10,1000,310), "Health: " + Health);		
		
	}
}
