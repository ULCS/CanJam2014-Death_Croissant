using UnityEngine;
using System.Collections;

public class GhostSpawn : MonoBehaviour {
	
	public GameObject ghost;
	public Transform ghostSpawn1;
	public Transform ghostSpawn2; 
	public Transform ghostSpawn3; 
	public GameObject parentGameObject;
	
	private float ghostSpawnTime;
	public float ghostSpawnInterval = 7.0f;
	public float ghostSpawnLimit = 100.0f;

		
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//ghostSpawnTime -= 1;
		
		if(ghostSpawnLimit >= 0 && Time.time > ghostSpawnTime)
		{ 			
			GameObject instObj;
			instObj = Instantiate(ghost,ghostSpawn1.position, ghostSpawn1.rotation) as GameObject;
			instObj = Instantiate(ghost,ghostSpawn2.position, ghostSpawn2.rotation) as GameObject;
			instObj = Instantiate(ghost,ghostSpawn3.position, ghostSpawn3.rotation) as GameObject;
			instObj.transform.parent = parentGameObject.transform;
			ghostSpawnTime = 5;
			ghostSpawnLimit -= 1;
			ghostSpawnTime = Time.time+ghostSpawnInterval;
		}	 		
	}
}
