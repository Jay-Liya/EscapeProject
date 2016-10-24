using UnityEngine;

//Class to hide the objects when starting
public class HideOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.SetActive(false);
	}

}
