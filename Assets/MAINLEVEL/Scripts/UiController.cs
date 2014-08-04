using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour {

    public tk2dUIItem nextScene;
	public tk2dUIItem Exit;
	public tk2dUIItem Info;	  
    public tk2dUIItem Back;
	public tk2dUIItem Credits;	  
    public tk2dUIItem Back2;
	public GameObject target;
	
	
	//public AudioSource Music;

	
	void Start () 
	{		
		Time.timeScale = 1.0f;
		//audio.Play();
	}
	
	void Update()
	{
			
	}
	
    void Awake()
    {
		audio.Play();
		Time.timeScale = 1.0f;
    }

    void OnEnable()
    {
        nextScene.OnClick += GoToMain;
		Exit.OnClick += GoToExit;
		Info.OnClick += GoToInfo;
		Back.OnClick += GoToMenu;
		Credits.OnClick += GoToCredits;
		Back2.OnClick += GoToMenu2;
    }

    void OnDisable()
    {
		
    }
	
	private void GoToCredits()
    {
        iTweenEvent.GetEvent(target, "gotocredits").Play();		
    }
	
	private void GoToMenu2()
    {
        iTweenEvent.GetEvent(target, "gotomenu2").Play();		
    }
	
	private void GoToInfo()
    {
        iTweenEvent.GetEvent(target, "infoto").Play();		
    }
	
	private void GoToMenu()
    {
        iTweenEvent.GetEvent(target, "infoaway").Play();		
    }

    private void GoToMain()
    {
        Application.LoadLevel("Level");		
    }

    private void GoToExit()
    {
    	Application.Quit();
    }

}
