using UnityEngine;

//Class for JSON Http
public class JSONHttp : MonoBehaviour {

	//Initialization
	void Start () {
	
	}

	private string _serverURL = "http://jsnDrop.com/jsndrop";

	public string getJSON<T>(T anObject){

		string result = JsonUtility.ToJson(anObject);
		return result;
	}
}
