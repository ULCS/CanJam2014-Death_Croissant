using UnityEngine;
using System.Collections;

public class ballGun : MonoBehaviour {
	
	public GameObject projectile;
	public Transform projectilePlaceHolder;
	private GameObject gunFireAudio;
	private GameObject gunReloadAudio;
	public float shotInterval = 0.6F;	
	public float reloadTime = 5.0f;
	private int AmmoLeft;
	private int Bullets = 10;
	private float shotTime;
	public GUISkin customSkin;
	private bool reloadGUI;
	public static bool isPaused = false;	
	private bool Torch = false;
	private float timeLeft = 60f;
	public static bool gameOver = false;
	public static bool byebye = false;
	public Texture2D crosshairImage;
	
	private GameObject Trackable;

	// Use this for initialization
	void Start () {	
		gunFireAudio = this.gameObject.transform.FindChild("gunFireAudio").gameObject;
		Trackable = GameObject.Find("ImageTarget").gameObject;

		if(Ball.Health <= 0)
		{
			Time.timeScale = 0.0f;
			byebye = true;	
		}

	}	
	//is called once per frame
	
	void Update () {
		
		if(Ball.Health <= 0)
		{
			Time.timeScale = 0.0f;
			byebye = true;	
		}

		//if(Bullets>0 && Time.time > shotTime && Input.GetMouseButtonDown(0) && !isPaused && !gameOver)
//		if(Input.GetMouseButtonDown(0) && !isPaused && !gameOver)
//		{ 			
//			Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
//			RaycastHit hit;
//			if (Physics.Raycast(ray, out hit, 1000))
//			{
//				print("I'm looking at " + hit.rigidbody.name);
//				hit.rigidbody.AddForce(Camera.main.transform.forward * 1000);
//			}
//			gunFireAudio.audio.Play();			
//		}
		
		if(Input.GetMouseButtonDown(0) && isPaused && !gameOver && !byebye)
		{
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			//Application.LoadLevel("Menu");
			Ball.Health = 500;
		}

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver && !byebye)
        {
			if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
			   {
			      Time.timeScale = 0.0f;
				  AudioListener.pause = true;
			      isPaused = true;
			   }
			   else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
			   {
			      Time.timeScale = 1.0f;
				  AudioListener.pause = false;
			      isPaused = false;    
			   }			
        }	
		
		if(Input.GetKeyDown(KeyCode.Escape) && gameOver)
		   {
				Time.timeScale = 1.0f;
				AudioListener.pause = false;
				Application.LoadLevel("Menu");  
				Ball.Health = 500;
		   } 
		
		if(Input.GetMouseButtonDown(0) && gameOver)
		{ 			
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			//Application.LoadLevel("Choose");
			Application.LoadLevel("Level");
			Ball.Health = 500;
			gameOver = false;
			byebye = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && byebye)
		   {
				Time.timeScale = 1.0f;
				AudioListener.pause = false;
				Application.LoadLevel("Menu");  
				Ball.Health = 500;
				gameOver = false;
				byebye = false;
		   } 
		
		if(Input.GetMouseButtonDown(0) && byebye)
		{ 			
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			Application.LoadLevel("Level");
			//Application.LoadLevel("Choose");  
			Ball.Health = 500;
			gameOver = false;
			byebye = false;
		}		
	}
	
	void OnGUI ()
	{
		GUI.skin = customSkin;
		
		GUI.Label (new Rect (Screen.width - 240,10,1000,310), "health: " + Ball.Health);	

		if(isPaused == true)
		{
		GUI.color = Color.cyan;	
		GUI.Label (new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 200, 70), "PAUSED");
		GUI.Label (new Rect(Screen.width / 2 - 240, Screen.height / 2 - 50, 600, 70), "Press the Back key to Resume");
		GUI.Label (new Rect(Screen.width / 2 - 85, Screen.height / 2 - 0, 600, 70), "Tap to Exit");
		}

		if(gameOver)
		{
			GUI.color = Color.green;
			GUI.Label (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 600, 70), "You Win!!");
			GUI.Label (new Rect(Screen.width / 2 - 160, Screen.height / 2 - 50, 600, 70), "Tap to try again");
			GUI.Label (new Rect(Screen.width / 2 - 270, Screen.height / 2 - 0, 600, 70), "Press the Back key to goto Menu");			
		}
		
		if(byebye)
		{
			GUI.color = Color.red;
			GUI.Label (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 600, 70), "You failed!");
			GUI.Label (new Rect(Screen.width / 2 - 160, Screen.height / 2 - 50, 600, 70), "Tap to try again");
			GUI.Label (new Rect(Screen.width / 2 - 270, Screen.height / 2 - 0, 600, 70), "Press the Back key to Give Up");			
		}
	}
}
