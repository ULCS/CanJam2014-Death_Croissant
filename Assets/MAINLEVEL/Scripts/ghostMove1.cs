using UnityEngine;
using System.Collections;

public class ghostMove1 : MonoBehaviour {
	
	public GameObject target;
	public Vector3 position;
	public float time;

	// Use this for initialization
	void Start () {
		//iTween.MoveTo(target, iTween.Hash("path", iTweenPath.GetPath("GhostSpawn1"), "time", 20,
		//	"orientToPath",true, "lookTime", 0.2));
		iTween.MoveTo(target,iTween.Hash("position",position,"time",time ,
			"orientToPath",true, "lookTime", 0.2));
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
	
}
