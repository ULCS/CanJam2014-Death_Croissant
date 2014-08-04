using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	
	public int damage;
	public static int Health = 200;
	public GUISkin customSkin;	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Health <= 0)
		{
			Destroy(gameObject);
		}
	
	}
	
	void OnTriggerEnter(Collider other)	
	{
		if(other.gameObject.tag == "Ghost" ) 
		{
        Destroy(other.gameObject);
		Health -= damage;
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
