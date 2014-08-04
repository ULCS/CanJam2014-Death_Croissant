using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter(Collider other)	
	{
		if(other.gameObject.tag == "Ghost" ) 
		{
        Destroy(other.gameObject);
		//other.gameObject.SetActive(false);
		//other.GetComponent(MeshRenderer).enabled = false;
		}
    }
}
