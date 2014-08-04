using UnityEngine;
using System.Collections;

public class particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
	
		transform.particleSystem.Play();
		//if(other.gameObject.tag == "Ghost" ) 
		//{
        //Destroy(other.gameObject);
		//other.gameObject.SetActive(false);
		//other.GetComponent(MeshRenderer).enabled = false;
		//}
    }

}
