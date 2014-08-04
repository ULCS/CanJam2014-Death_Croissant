using UnityEngine;
using System.Collections;

public class Turret1fireDam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerStay(Collider other)	
	{
		if (other.gameObject.tag == "Player") {
			Ball.Health -= 1;
			//Destroy(other.gameObject);		
			//other.gameObject.SetActive(false);
			//other.GetComponent(MeshRenderer).enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
