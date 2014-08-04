using UnityEngine;using System.Collections;
public class PredictionPath : MonoBehaviour 
{
	// Use this for initialization
	public Transform Cube; //This is the prefab we will be instantiating as the path steps
	Transform[] Cubes; //the array that will hold all the gameobjects that draw the path(path steps)
	public Transform ShootingPoint; //Shooting Point
	GameObject TidyParent; //just an empty game object to instantiate the path steps inside of it to keep things clean
	public Rigidbody Bullet; //the bullet rigidbody
	Vector3 Gravity; //gravity
	public float FrequencyMultiplier; //this will increase or decrease spacing between path steps
	public int Ammount; //ammount of path steps
	public Vector3 InitialVelocity; //the velocity by which you will shoot the projectile

	void Start () 
	{
		Gravity = Physics.gravity; //sets our gravity variable to the defined project's gravity
		Cubes = new Transform[Ammount]; //creates a new array and sets its size to the Ammount variable
		TidyParent = new GameObject ("TidyParent"); //create empty gameobject
		for (int i=0; i<Ammount-1; i++) 
		{ //Instantiate Path Steps and save them in the array
			Cubes[i] = Instantiate(Cube,Vector3.zero,Quaternion.identity) as Transform;
			Cubes[i].parent = TidyParent.transform;
		}
	}

	void Update () {
		UpdatePath ();
		if (Input.GetKeyUp (KeyCode.Space)) 
		{ //Shoots the bullet for demonstration
			Rigidbody Clone = Instantiate(Bullet,ShootingPoint.position,Quaternion.identity) as Rigidbody;
			Clone.AddForce(InitialVelocity,ForceMode.VelocityChange); 
			//THIS IS VERY IMPORTANT you must use ForceMode.VelocityChange because it doesnt depend on the mass unlike ForceMode.Impulse and it adds the velocity instantly, 
			//if you use the default forcemode the prediction wont work.
		}
	}

	void UpdatePath(){
		for (int i = 0; i < Ammount - 1; i++) 
		{ //loop through the array and set each path step to its predicted position
			Vector3 PredictedPosition = PlotPath(ShootingPoint.position,InitialVelocity,i * FrequencyMultiplier);
			Cubes[i].position = PredictedPosition;
		}
	}

	Vector3 PlotPath(Vector3 InitialPosition, Vector3 InitialVelocity, float TimeStep)
	{
		return InitialPosition + ((InitialVelocity * TimeStep) + (0.5f * Gravity * (TimeStep * TimeStep))); 
		//this is the very simple equation of motion s=ut+1/2at^2 just with Vector3 in 3 dimensions not one dimension as usual
	}
}