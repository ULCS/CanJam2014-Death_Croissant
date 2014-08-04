using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)	
			{
				if(other.gameObject.tag == "Player" ) 
				{
					ballGun.gameOver = true;
				}
			}

	// Update is called once per frame
	void Update () {
	
	}
}
