using UnityEngine;
//use to handle the camera animation
public class CameraRoot : MonoBehaviour {

	void Awake(){

		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {

		GameModel.makeScenes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
