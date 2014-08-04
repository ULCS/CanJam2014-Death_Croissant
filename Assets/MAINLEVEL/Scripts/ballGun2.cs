using UnityEngine;
using System.Collections;

public class ballGun2 : MonoBehaviour {
	
	public GameObject projectile;
	//public GameObject projectileLight;
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
	private bool isPaused = false;	
	private bool Torch = false;
	private float timeLeft = 60f;
	private bool gameOver = false;
	private bool byebye = false;
	public Texture2D crosshairImage;
	
	private GameObject Trackable;

	// Use this for initialization
	void Start () {	
		gunFireAudio = this.gameObject.transform.FindChild("gunFireAudio").gameObject;
		gunReloadAudio = this.gameObject.transform.FindChild("gunReloadAudio").gameObject;
		Trackable = GameObject.Find("ImageTarget").gameObject;
		//GameObject go = GameObject.Find ("HALL13_Belfya");
		//Target target = go.GetComponent <Target> ();
		//int theHealth = target.Health;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Target.Health <= 0)
		{
			Time.timeScale = 0.0f;
			byebye = true;	
		}
		
		if(timeLeft <= 0)
		{
			Time.timeScale = 0.0f;
			gameOver = true;	
		}
		
		if(timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
		}
		
		if(Bullets == 10 && Time.time > shotTime)
		{
			reloadGUI = false;	
		}
		if(Bullets>0 && Time.time > shotTime && Input.GetMouseButtonDown(0) && !isPaused && !gameOver)
		{ 			
			Bullets -= 1;
			gunFireAudio.audio.Play();
			GameObject obj = Instantiate(projectile, projectilePlaceHolder.position, this.gameObject.transform.rotation) as GameObject;
			obj.gameObject.rigidbody.AddRelativeForce(Vector3.forward * Time.deltaTime * 1100000); 
			obj.transform.parent = Trackable.transform;
			Destroy(obj.gameObject,5.0f);
			shotTime = Time.time+shotInterval;	
			
		}
		if (Bullets==0)
		{
			reloadGUI = true;
			gunFireAudio.audio.Pause();
			gunReloadAudio.audio.Play();
		    Bullets += 10; // load the bullets		    
		    shotTime = Time.time + reloadTime; // set reload time			
		}
		
		if(Input.GetMouseButtonDown(0) && isPaused && !gameOver)
		{
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			Application.LoadLevel("Menu");
			Target.Health = 200;
		}
		
		if(Input.GetKeyDown(KeyCode.Menu) && isPaused && Torch)
			   {
			      CameraDevice.Instance.SetFlashTorchMode(false);
				  Torch = false;  
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
				Target.Health = 200;
		   } 
		
		if(Input.GetMouseButtonDown(0) && gameOver)
		{ 			
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			Application.LoadLevel("Choose");
			Target.Health = 200;
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && byebye)
		   {
				Time.timeScale = 1.0f;
				AudioListener.pause = false;
				Application.LoadLevel("Menu");  
				Target.Health = 200;
		   } 
		
		if(Input.GetMouseButtonDown(0) && byebye)
		{ 			
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			Application.LoadLevel("Choose");  
			Target.Health = 200;
		}
		
		if (Input.touchCount == 2 && isPaused && !Torch)
		{
            CameraDevice.Instance.SetFlashTorchMode(true);
			Torch = true;
		}
		
				
	}
	
	//void OnTriggerEnter(Collider other)	
	//{
	//	if(other.gameObject.tag == "Ghost" ) 
	//	{
     //   	Destroy(other.gameObject);		
		//other.gameObject.SetActive(false);
		//other.GetComponent(MeshRenderer).enabled = false;
	//	}
    //}
	
	
	void OnGUI ()
	{
		GUI.skin = customSkin;

		float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
		
		GUI.Label (new Rect (Screen.width - 220,10,1000,310), "Health: " + Target.Health);	
		
		GUI.Label (new Rect (20,10,1000,300), "Ammo: " + Bullets);	
		GUI.Label (new Rect(Screen.width / 2 - 60, 10, 310, 100), "Time: " + (int)timeLeft);
		
		if(isPaused == false)
		{
			GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
		}

		if(isPaused == true)
		{
		GUI.color = Color.cyan;	
		GUI.Label (new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 200, 70), "PAUSED");
		GUI.Label (new Rect(Screen.width / 2 - 240, Screen.height / 2 - 50, 600, 70), "Press the Back key to Resume");
		GUI.Label (new Rect(Screen.width / 2 - 85, Screen.height / 2 - 0, 600, 70), "Tap to Exit");
		}
		
		if(reloadGUI == true)
		{
			GUI.color = Color.red;
			GUI.Label (new Rect (10,70,1000,300), "RELOADING");
		}	
		
		if(byebye)
		{
			GUI.color = Color.red;
			GUI.Label (new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 600, 70), "You lost the Muffins!");
			GUI.Label (new Rect(Screen.width / 2 - 160, Screen.height / 2 - 50, 600, 70), "Tap to play more");
			GUI.Label (new Rect(Screen.width / 2 - 230, Screen.height / 2 - 0, 600, 70), "Press the Back key to Quit");			
		}
		
		 if(gameOver)
		{
			GUI.color = Color.green;
			GUI.Label (new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 600, 70), "You saved the Muffins!");
			GUI.Label (new Rect(Screen.width / 2 - 160, Screen.height / 2 - 50, 600, 70), "Tap to play more");
			GUI.Label (new Rect(Screen.width / 2 - 230, Screen.height / 2 - 0, 600, 70), "Press the Back key to Quit");			
		}
	}
}
