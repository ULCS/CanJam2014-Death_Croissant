using UnityEngine;	
using System.Collections;

public class Turret1fire : MonoBehaviour		
{		
	public int segments;		
	public float radius;		
	LineRenderer line;
	public float health = 5;

	void Start ()			
	{			
		line = gameObject.GetComponent<LineRenderer>();		
		
		line.SetVertexCount (segments + 1);			
		line.useWorldSpace = false;			
		CreatePoints ();		
	}

	void Update ()
	{
		if (Input.GetMouseButton(0))
		{
			//Creating container for the raycast result
			RaycastHit hitInfo = new RaycastHit();
			//Making the raycast
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
			{					
				if (hitInfo.collider.gameObject.tag == "Bullet") 
				{						
					health -= 1 * Time.deltaTime;
				}					
			}
		}

		if (health <= 0)
			Destroy (this.gameObject);
	}

	void CreatePoints ()			
	{			
		float x;			
		float y;			
		float z = 0f;	
		float angle = 0f;
		
		for (int i = 0; i < (segments + 1); i++)				
		{				
			x = Mathf.Sin (Mathf.Deg2Rad * angle);				
			y = Mathf.Cos (Mathf.Deg2Rad * angle);				
			line.SetPosition (i,new Vector3(x,y,z) * radius);				
			angle += (360f / segments);				
		}			
	}		
}