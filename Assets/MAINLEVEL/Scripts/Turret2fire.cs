using UnityEngine;
using System.Collections;

public class Turret2fire : MonoBehaviour {

	private Vector3 destPos;
	public Transform bulletSpawnPoint;
	private Transform playerTransform;
	public float curRotSpeed;

	public float shotInterval;
	private float shotTime;
	public GameObject projectile;

	public float health = 5;
	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0F;

	// Use this for initialization
	void Start () {
	
		GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		playerTransform = objPlayer.transform;		
		bulletSpawnPoint = gameObject.transform.GetChild(0).transform;
	}

	// Update is called once per frame
	void Update () {

		float lerp = Mathf.PingPong(Time.deltaTime, duration) / duration;
		renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);

		destPos = playerTransform.position;

		Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
	
		if (Input.GetMouseButton(0))
		{
			//Creating container for the raycast result
			RaycastHit hitInfo = new RaycastHit();
			//Making the raycast
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
			{					
				if (hitInfo.collider.gameObject.tag == "Bullet2") 
				{
					//Debug.Log( "ray hit (tag): " + hitInfo.collider.gameObject.tag );
					health -= 1 * Time.deltaTime;
				}					
			}
		}

		if (health <= 0)
			Destroy (this.gameObject);

		if(Time.time > shotTime && !ballGun.isPaused && !ballGun.gameOver)
		{ 
			GameObject obj = Instantiate(projectile, bulletSpawnPoint.position, this.gameObject.transform.rotation) as GameObject;
			obj.gameObject.rigidbody.AddRelativeForce(Vector3.forward * Time.deltaTime * 1500000); 
			//obj.transform.parent = Trackable.transform;
			Destroy(obj.gameObject, 3.0f);
			shotTime = Time.time+shotInterval;	
			
		}
	}
}
