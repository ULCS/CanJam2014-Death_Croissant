using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Rigidbody projectile;
	public float distance = 1.0f;
	public Transform spawnTarget;
	public LineRenderer predictionLine;
	public bool enablePrediction = true;
	public bool targetPoint = true;
	
	private float horizontalOffset = 0.0f;
	private float lob = 0.75f;
	private Vector3 startingVelocity;

	// Use this for initialization
	void Start () {
		foreach(GUIText text in GameObject.FindObjectsOfType(typeof(GUIText))) {
			text.font.material.color = Color.black;	
		}
		
		animation.CrossFade("Idle");
	}

	void OnTriggerEnter(Collider collision)
	{
		//Reduce health
		if(collision.gameObject.tag == "Bomb")
			Ball.Health -= 15;
	}   
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			predictionLine.enabled = true;
		}
		else {
			predictionLine.enabled = false;
		}

		if(targetPoint) {
			startingVelocity = GetTrajectoryVelocity(spawnTarget.position, GetMousePosition(), lob, Physics.gravity);	
		}
		else {
			startingVelocity = Quaternion.AngleAxis(horizontalOffset, Vector3.up) * (transform.forward * (20f * distance));
		}
		
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			FireProjectile();
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			horizontalOffset -= 25 * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			horizontalOffset += 25 * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			distance += 0.75f * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			distance -= 0.75f * Time.deltaTime;
		}
		
		horizontalOffset = Mathf.Clamp(horizontalOffset, -45.0f, 35.0f);
		
		lob += Input.GetAxis("Mouse ScrollWheel") * 0.01f;
		
		lob = Mathf.Clamp(lob, 0.25f, 1.2f);
		distance = Mathf.Clamp(distance, 0.25f, 1.5f);
		
		if(enablePrediction)
			UpdatePredictionLine();
	}
	
	void UpdatePredictionLine() 
	{
		predictionLine.SetVertexCount(180);
		Vector3 previousPosition = spawnTarget.position;
		for(int i = 0; i < 180; i++)
		{
			Vector3 currentPosition = GetTrajectoryPoint(spawnTarget.position, startingVelocity, i, 1, Physics.gravity);
			Vector3 direction = currentPosition - previousPosition;
			direction.Normalize();
			
			float distance = Vector3.Distance(currentPosition, previousPosition);
			
			RaycastHit hitInfo = new RaycastHit();
			if(Physics.Raycast(previousPosition, direction, out hitInfo, distance))
			{
				predictionLine.SetPosition(i,hitInfo.point);
				predictionLine.SetVertexCount(i);
				break;
			}
			
			previousPosition = currentPosition;
			predictionLine.SetPosition(i,currentPosition);
		}
	}
	
	Vector3 GetMousePosition()
	{
		Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit[] hits = Physics.RaycastAll(ray);
		
		if(hits.Length > 0)
			return hits[0].point;
		else
			return Vector3.zero;
	}
	
	Vector3 GetTrajectoryPoint(Vector3 startingPosition, Vector3 initialVelocity, float timestep, float lob, Vector3 gravity)
	{
		float physicsTimestep = Time.fixedDeltaTime;
		Vector3 stepVelocity = initialVelocity * physicsTimestep;
		
		//Gravity is already in meters per second, so we need meters per second per second
		Vector3 stepGravity = gravity * physicsTimestep * physicsTimestep;
		
		return startingPosition + (timestep * stepVelocity) + ((( timestep * timestep + timestep) * stepGravity ) / 2.0f);
	}
	
	public static Vector3 GetTrajectoryVelocity(Vector3 startingPosition, Vector3 targetPosition, float lob, Vector3 gravity)
	{
		float physicsTimestep = Time.fixedDeltaTime;
		float timestepsPerSecond = Mathf.Ceil(1f/physicsTimestep);
		
		//By default we set n so our projectile will reach our target point in 1 second
		float n = lob * timestepsPerSecond;
		
		Vector3 a = physicsTimestep * physicsTimestep * gravity;
		Vector3 p = targetPosition;
		Vector3 s = startingPosition;
		
		Vector3 velocity = (s + (((n * n + n) * a) / 2f) - p) * -1 / n;
		
		//This will give us velocity per timestep. The physics engine expects velocity in terms of meters per second
		velocity /= physicsTimestep;
		return velocity;
	}
	
	void FireProjectile() {
		Rigidbody rb = (Rigidbody)Instantiate(projectile);
		rb.transform.position = spawnTarget.position;
		
		rb.AddForce(startingVelocity, ForceMode.Impulse);
	}
}
