using UnityEngine;
using System.Collections;

public class Turret4 : MonoBehaviour {
	
	public float health = 5;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			//Creating container for the raycast result
			RaycastHit hitInfo = new RaycastHit();
			//Making the raycast
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
			{					
				if (hitInfo.collider.gameObject.tag == "Bullet4") 
				{
					health -= 1 * Time.deltaTime;
				}					
			}
		}
		
		if (health <= 0)
			Destroy (this.gameObject);
		
	}
}
